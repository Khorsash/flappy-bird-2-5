using Godot;
using System;
using System.Collections.Generic;

public partial class MapChooser : Control
{
	public string Map;
	[Signal]
	public delegate void MapChangedEventHandler();
	public Queue<string> maps; 
	public Label mapNameLabel;
	public override void _Ready()
	{
		maps = new Queue<string>();
		foreach(Node n in GetChildren())
			if(n is Sprite2D s)
				maps.Enqueue(s.Name);
		
		while(maps.Peek() != "Map1") maps.Enqueue(maps.Dequeue());
		Map = maps.Peek();
		mapNameLabel = GetNode<Label>("MapName");
		ShowMap(Map);
		EmitSignal(SignalName.MapChanged);
		GetNode<Button>("Next").ButtonUp += NextMap;
		GetNode<Button>("Previous").ButtonUp += PreviousMap;
		// string[] files = ResourceLoader.ListDirectory(path);
	}

	public void ShowMap(string newMap)
	{
		if(!maps.Contains(newMap)) return;
		GetNode<Sprite2D>(Map).Hide();
		GetNode<Sprite2D>(newMap).Show();
		mapNameLabel.Text = newMap;
	}
	public void NextMap()
	{
		maps.Enqueue(maps.Dequeue());
		ShowMap(maps.Peek());
		Map = maps.Peek();
		EmitSignal(SignalName.MapChanged);
	}
	public void PreviousMap()
	{
		for(int i=0; i<maps.Count-1; i++) maps.Enqueue(maps.Dequeue());
		ShowMap(maps.Peek());
		Map = maps.Peek();
		EmitSignal(SignalName.MapChanged);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
