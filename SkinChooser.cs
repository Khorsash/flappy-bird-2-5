using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class SkinChooser : Control
{
	// Called when the node enters the scene tree for the first time.
	public string Skin;
	[Signal]
	public delegate void SkinChangedEventHandler();
	public Queue<string> skins; 
	public Label skinNameLabel;
	public override void _Ready()
	{
		skins = new Queue<string>();
		foreach(Node n in GetChildren())
			if(n is AnimatedSprite2D a)
				skins.Enqueue(a.Name);
		Skin = "Bird";
		skinNameLabel = GetNode<Label>("SkinName");
		ShowSkin(Skin);
		EmitSignal(SignalName.SkinChanged);
		GetNode<Button>("Next").ButtonUp += NextSkin;
		GetNode<Button>("Previous").ButtonUp += PreviousSkin;
	}

	public void ShowSkin(string newSkin)
	{
		if(!skins.Contains(newSkin)) return;
		GetNode<AnimatedSprite2D>(Skin).Hide();
		GetNode<AnimatedSprite2D>(newSkin).Show();
		skinNameLabel.Text = newSkin;
	}
	public void NextSkin()
	{
		skins.Enqueue(skins.Dequeue());
		ShowSkin(skins.Peek());
		Skin = skins.Peek();
		EmitSignal(SignalName.SkinChanged);
	}
	public void PreviousSkin()
	{
		for(int i=0; i<skins.Count-1; i++) skins.Enqueue(skins.Dequeue());
		ShowSkin(skins.Peek());
		Skin = skins.Peek();
		EmitSignal(SignalName.SkinChanged);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
