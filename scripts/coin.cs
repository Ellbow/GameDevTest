using Godot;
using System;

public partial class coin : Area2D
{
	int count = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		BodyEntered += OnBodyEntered;

		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		
	}

	private void OnBodyEntered(Node body)
	{
		count++;
		GD.Print("body entereed " + count);
	}
}
