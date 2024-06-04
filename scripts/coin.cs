using Godot;
using System;
using System.Threading;

public partial class coin : Area2D
{
	int coinValue = 1;

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
		var gameManager = GetNode<GameManager>("/root/GameManager");
    	gameManager.IncreaseScore(coinValue);
		GD.Print(gameManager.GetScore());
		QueueFree();
	}
}
