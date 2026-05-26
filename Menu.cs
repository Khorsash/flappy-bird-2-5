using Godot;
using System;

public partial class Menu : Control
{
	// Called when the node enters the scene tree for the first time.
	[Signal]
	public delegate void PlayButtonPressedEventHandler();
	public override void _Ready()
	{
		GetNode<Button>("PlayButton").ButtonUp += OnPlayButtonUp;
	}

	public void OnPlayButtonUp()
	{
		EmitSignal(SignalName.PlayButtonPressed);
	} 

	public void PlayAgain()
	{
		Show();
		GetNode<Button>("PlayButton").Text = "Play Again";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
