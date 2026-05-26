using Godot;
using System;
using System.ComponentModel;

public partial class BottomBorder : Area2D
{
	[Signal]
	public delegate void TouchedBorderEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += onEnter;
	}

	public void onEnter(Area2D area2D)
	{
		if(area2D.Name == "trigger") 
			EmitSignal(SignalName.TouchedBorder);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
