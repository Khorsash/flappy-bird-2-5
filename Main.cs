using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.

	
	public Bird bird;
	public PackedScene cgs;
	public Menu menu;
	public Score scorelabel;
	public SkinChooser skinChooser;
	public Vector2 ScreenSize;
	public bool isRunning;
	public Tm hardnessTimer;
	public Tm nextColumnTimer;
	public double decrNCTValueBy; // where NCT is Next Column Time
	public int score = 0;
	public float startHardness = 2;
	public float hardness = 2;
	public float maxHardness = 5;
	[Export]
	public int hardnessIncreaseSpeed = 100; // which can be also called count of hardness increasings
	[Export]
	public float ColumnSpeed = 200;
	public AudioStreamPlayer audi0;
	public AudioStreamMP3 bg;
	public AudioStreamMP3 jsjp;
	public override void _Ready()
	{
		decrNCTValueBy = (4-0.4)/ColumnSpeed;
		hardnessTimer = new Tm(20);
		nextColumnTimer = new Tm(4);
		bird = GetNode<Bird>("Bird");
		menu = GetNode<Menu>("Menu");
		scorelabel = GetNode<Score>("Score");
		skinChooser = GetNode<SkinChooser>("SkinChooser");
		cgs = GD.Load<PackedScene>("res://column_group.tscn");
		ScreenSize = GetViewport().GetVisibleRect().Size;
		menu.PlayButtonPressed += StartGame;
		skinChooser.SkinChanged += () => bird.SetSkin(skinChooser.Skin);
		isRunning = false;
		audi0 = GetNode<AudioStreamPlayer>("audi");
		bg = GD.Load<AudioStreamMP3>("res://audio/bg.mp3");
		jsjp = GD.Load<AudioStreamMP3>("res://audio/jsjp.mp3");
		jsjp.Loop = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!isRunning) return;
		hardnessTimer.Update(delta);
		nextColumnTimer.Update(delta);
		if(hardnessTimer.HasEllapsed()) 
		{
			hardnessTimer.Reset();
			ColumnSpeed = ColumnSpeed / hardness * startHardness;
			if(hardness < maxHardness) 
				hardness += maxHardness/hardnessIncreaseSpeed;
			ColumnSpeed = ColumnSpeed * hardness / startHardness;
		}
		if(nextColumnTimer.HasEllapsed()) 
		{
			nextColumnTimer.Reset(nextColumnTimer.duration-decrNCTValueBy);
			AddColumnGroup();

		}
	}

	public void OnGameOver()
	{
		GD.Print("YOU LOSE");
		audi0.Stop();
		audi0.Stream = bg;
		audi0.Play();
		isRunning = false;
		bird.isRunning = false;
		foreach(Node n in GetChildren()) 
			if(n is ColumnGroup cg)
				cg.QueueFree();
		menu.PlayAgain();
		skinChooser.Show();
	}

	public void StartGame()
	{
		menu.Hide();
		skinChooser.Hide();
		scorelabel.Show();
		bird.isRunning = true;
		bird.SetHeight(ScreenSize.Y/10);
		score = 0;
		hardness = 2;
		ColumnSpeed = 200;
		hardnessTimer = new Tm(20);
		nextColumnTimer = new Tm(4);
		scorelabel.SetScore(score);
		isRunning = true;
		GetNode<BottomBorder>("bottom_border").TouchedBorder += OnGameOver;
		GetNode<LeftBorder>("left_border").ColumnGroupEnter += DisconnectFromColumnGroupEvents;
		audi0.Stop();
		audi0.Stream = jsjp;
		audi0.Play();
	}

	public void DisconnectFromColumnGroupEvents(ColumnGroup cg)
	{
		cg.GetNode<Column>("c1").TouchedColumn -= OnGameOver;
		cg.GetNode<Column>("c2").TouchedColumn -= OnGameOver;
		cg.GetNode<ColumnBorder>("ColumnBorder").PointAchieved -= AddOneScore;
		RemoveChild(cg);
		cg.QueueFree();
		int c = 0;
		foreach(Node n in GetChildren()) 
			if(n is ColumnGroup) c++;
		GD.Print($"Column count: {c}");
	}
	
	public void AddOneScore()
	{
		score += 1;
		scorelabel.SetScore(score);
	}
	public void AddColumnGroup()
	{
		ColumnGroup cg = cgs.Instantiate<ColumnGroup>();
		cg.SetHardness(hardness, ScreenSize.Y);
		cg.SetSpeed(ColumnSpeed);
		cg.GetNode<Column>("c1").TouchedColumn += OnGameOver;
		cg.GetNode<Column>("c2").TouchedColumn += OnGameOver;
		cg.GetNode<ColumnBorder>("ColumnBorder").PointAchieved += AddOneScore;
		AddChild(cg);
		cg.Position = new Vector2(ScreenSize.X, 0);
		GD.Print("New Column");
	}
}
