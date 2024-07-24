using Godot;
using System;

public partial class player : CharacterBody2D
{
    public const float Speed = 150.0f;
    public const float JumpVelocity = -250.0f;

    // Reference to the Label node
    [Export]
    public Label StatusLabel;

    public float Gravity => ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    private AnimatedSprite2D sprite => GetNode<AnimatedSprite2D>("AnimatedSprite2D");

    private StateMachine<player> StateMachine;

    public override void _Ready()
    {
        StatusLabel = new Label();
        StateMachine = new StateMachine<player>(this);

        StateMachine.SetState(StateMachine<player>.State.Idle);


        var fontFile = ResourceLoader.Load<FontFile>("res://assets/fonts/PixelOperator8-Bold.ttf");
        StatusLabel.AddThemeFontOverride("font", fontFile);
        StatusLabel.AddThemeFontSizeOverride("font_size", 8);
        StatusLabel.Text = "Initialising State";
        StatusLabel.Position = new Vector2(-20, -20); // Adjust the offset as needed
        StatusLabel.Modulate = new Color(1,0,0); // White color

        // Add the label as a child to this node
        AddChild(StatusLabel);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add gravity if not on the floor
        if (!IsOnFloor())
            velocity.Y += Gravity * (float)delta;

        // Handle jump
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
            //StateMachine.TransitionToState(StateMachine<player>.State.Jumping);
        }

        // Get input direction
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

        // Handle sprite flipping
        if (direction.X > 0)
        {
            sprite.FlipH = false;
        }
        else if (direction.X < 0)
        {
            sprite.FlipH = true;
        }

        // Handle state transitions based on input and current state
        if (IsOnFloor())
        {
            if (direction.X == 0)
            {
                StateMachine.TransitionToState(StateMachine<player>.State.Idle);
            }
            else
            {
                // if (Input.IsActionPressed("Sprint"))
                // {
                //     StateMachine.TransitionToState(StateMachine<player>.State.Running);
                //     velocity.X = direction.X * Speed * 2; // Double speed for running
                // }
                //else
                //{
                    StateMachine.TransitionToState(StateMachine<player>.State.Walking);
                    velocity.X = direction.X * Speed;
                //}
            }
        }
        else if (StateMachine.CurrentState != StateMachine<player>.State.Jumping)
        {
            StateMachine.TransitionToState(StateMachine<player>.State.Jumping);
        }


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
        StatusLabel.Text = StateMachine.CurrentState.ToString();
    }

    public void OnEnterIdle()
    {
        sprite.Play("idle");
    }

    public void OnEnterWalking()
    {
        sprite.Play("run");
    }

    public void OnEnterRunning()
    {
        sprite.Play("run");
    }

    public void OnEnterJumping()
    {
        sprite.Play("jumping");
    }
}
