using Godot;
using System;
using System.Net.Http.Headers;

public partial class ColumnGroup : Area2D
{
	// Called when the node enters the scene tree for the first time.

	public float hardness = 2;
	public PackedScene cs;
	public float Speed = 0;
	public override void _Ready()
	{
	}
	

	public void SetHardness(float nhardness, float vh)
	{
		cs = GD.Load<PackedScene>("res://column.tscn");
		hardness = nhardness;
		Column c1 = cs.Instantiate<Column>();
		Column c2 = cs.Instantiate<Column>();
		c1.SetOr(false);
		c2.SetOr(true);
		c1.Name = "c1";
		c2.Name = "c2";
		AddChild(c1);
		AddChild(c2);

		float gapSize = vh / hardness;
		float minCenter = gapSize / 2 + vh / 2;
		float maxCenter = vh - gapSize / 2 - vh / 2;

		Random r = new Random();
		float center = minCenter + (float)r.NextDouble() * (maxCenter - minCenter);

		c1.SetHeight(center - gapSize / 2);
		c2.SetHeight(center + gapSize / 2 + vh / 2);
	}

	public void SetSpeed(float speed)
	{
		Speed = speed;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = new Vector2((float)(Position.X-Speed*delta), Position.Y);
	}
}
