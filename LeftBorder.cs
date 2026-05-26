using Godot;
using System;

public partial class LeftBorder : Area2D
{
	// Called when the node enters the scene tree for the first time.
	[Signal]
	public delegate void ColumnGroupEnterEventHandler(ColumnGroup cg);
	public override void _Ready()
	{
		
	}

	public void OnEnter(Area2D area2D)
	{
		if(area2D is ColumnGroup cg) 
			EmitSignal(SignalName.ColumnGroupEnter, cg);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
