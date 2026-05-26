using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public AudioStreamPlayer audi;

	public bool isRunning = false;

	public override void _Ready()
	{
		var stream = GD.Load<AudioStreamMP3>("res://mp.mp3");
		audi = GetNode<AudioStreamPlayer>("audi");
		audi.Stream = stream;
	}

	public void SetHeight(float h)
	{
		Position = new Vector2(Position.X, h);
	}
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_accept"))
		{
			audi.Stop();
			audi.Play();
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		if(!isRunning) return;

		Vector2 velocity = Velocity;

		velocity += GetGravity() * (float)delta;
		

		if (Input.IsActionJustPressed("ui_accept"))
		{
			velocity.Y = JumpVelocity;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
