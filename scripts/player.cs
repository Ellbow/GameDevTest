using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 150.0f;
	public const float JumpVelocity = -250.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	AnimatedSprite2D sprite => GetNode<AnimatedSprite2D>("AnimatedSprite2D");

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		if (direction.X > 0)
		{
			sprite.FlipH = false;
		}
		else if (direction.X < 0)
		{
			sprite.FlipH = true;
		}

		if (IsOnFloor())
		{
			if (direction.X == 0)
				sprite.Play("idle");
			else
				sprite.Play("run");
		}
		else
			sprite.Play("jumping");
		


		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
