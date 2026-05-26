using Godot;
using System;

public partial class ColumnBorder : Area2D
{
	// Called when the node enters the scene tree for the first time.
	[Signal]
	public delegate void PointAchievedEventHandler();
	public override void _Ready()
	{
		AreaEntered += OnEnter;
	}

	public void OnEnter(Area2D area2D)
	{
		if(area2D.Name == "trigger") 
			EmitSignal(SignalName.PointAchieved);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
