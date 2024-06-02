using Godot;
using System;

public partial class green_slime : Node2D
{
	double speed = 60;
	double direction = 1;
	private RayCast2D rayCastRight;
    private RayCast2D rayCastLeft;

	private AnimatedSprite2D sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // Access RayCast2D nodes by their names
        rayCastRight = GetNode<RayCast2D>("RayCastRight");
        rayCastLeft = GetNode<RayCast2D>("RayCastLeft");
        sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		// Optionally, set their CastTo vectors if not already set in the editor
        // rayCastRight.CastTo = new Vector2(10, 0); // 10 pixels ahead
        // rayCastLeft.CastTo = new Vector2(-10, 0); // 10 pixels behind
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		if (rayCastRight.IsColliding())
        {
            direction = -1;
			sprite.FlipH = true;
        }
		if (rayCastLeft.IsColliding())
        {
            direction = 1;
			sprite.FlipH = false;
        }

		//Use the with expression c# 10 feature "Nondestructive mutation creates a new object with modified properties"
		// var newPosition = Position;
		// Position = Position with { X = newPosition.X++ }; //Can also use a static value from another source to be accounted for e.g. knockback

		//Preferred syntax? Seems less computationally expensive than above?
		//TODO: Implement a global script that allows access to shared functions e.g. cast as float type
		var newPosition = Position;
		newPosition.X += (float)(direction * speed * delta);
		Position = newPosition;
	}
}
