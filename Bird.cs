using Godot;
using System;
using System.Collections.Generic;

public partial class Bird : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public AudioStreamPlayer audi;

	public bool isRunning = false;

	// [Export(PropertyHint.Enum, "Bird,SerbKitty,Sajkaca")]
	public string Skin = "Bird";
	public AnimatedSprite2D skinSprite;
	public override void _Ready()
	{
		var stream = GD.Load<AudioStreamMP3>("res://audio/mp.mp3");
		audi = GetNode<AudioStreamPlayer>("audi");
		audi.Stream = stream;
		SetSkin(Skin);
	}

	public void SetHeight(float h)
	{
		Position = new Vector2(Position.X, h);
	}
	public void SetSkin(string skin)
	{
		skinSprite = GetNode<AnimatedSprite2D>(skin);
		GetNode<AnimatedSprite2D>(Skin).Hide();
		skinSprite.Show();
		Skin = skin;
	}
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_accept"))
		{
			audi.Stop();
			audi.Play();
			
		}
		if (Input.IsActionJustPressed("ui_accept"))
		{
			skinSprite.Stop();
			skinSprite.Animation = "jump";
			skinSprite.Play();
		}
		else if (Input.IsActionJustReleased("ui_accept") && skinSprite.Animation == "jump")
		{
			skinSprite.Stop();
			skinSprite.Animation = "default";
			skinSprite.Play();
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
