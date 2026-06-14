using Godot;
using System;
using System.Collections.Generic;

public partial class Column : Area2D
{
	// Called when the node enters the scene tree for the first time.
	[Signal]
	public delegate void TouchedColumnEventHandler();
	// float height = GetViewport().GetVisibleRect().Size.Y;
	Dictionary<string, Tuple<Texture2D, Texture2D>> txtrs;
	public bool isUp;
	public string mapName = "";
	public override void _Ready()
	{
		AreaEntered += OnEnter;
	}
	public void SetOr(bool isup)
	{
		if(isup) GetNode<Sprite2D>("Columnup"+mapName).Show();
		else GetNode<Sprite2D>("Columndown"+mapName).Show();
		isUp = isup;
	}
	public void HideCurrOr()
	{
		if(isUp) GetNode<Sprite2D>("Columnup"+mapName).Hide();
		else GetNode<Sprite2D>("Columndown"+mapName).Hide();
	}
	public void SetMap(string nmapName)
	{
		HideCurrOr();
		mapName = nmapName;
		SetOr(isUp);
	}	
	public void OnEnter(Area2D area2D)
	{
		if(area2D.Name == "trigger") 
			EmitSignal(SignalName.TouchedColumn);
	}

	public void SetHeight(float h)
	{
		Position = new Vector2(Position.X, h);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Position += new Vector2(Speed, 0);
	}
}
