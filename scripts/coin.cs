using Godot;
using System;
using System.Threading;

public partial class coin : Area2D
{
	int coinValue = 1;
	private AudioStreamPlayer2D audioPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		audioPlayer = GetNode<AudioStreamPlayer2D>("PickupSound");
		audioPlayer.Finished += QueueFree; //directly delete object once the audio source has finished playing
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
		audioPlayer.Play();
	}

	private void OnSoundFinished() //Alternative method that can be called to play when a sound is finished, should be hooked up to an audio stream in _Ready()
    {
        // Free the node
        QueueFree();
    }
}
