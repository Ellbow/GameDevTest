using Godot;
using System;

public partial class player : CharacterBody2D
{
    public StateManager StateManager { get; private set; } = new StateManager();

        // Reference to the Label node
    [Export]
    public Label StatusLabel;

    public Vector2 velocity = Vector2.Zero;
    public float Gravity => ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public float Speed = 100f;

    public float WalkSpeed = 100f;
    public float RunSpeed = 200f;    
    public const float JumpVelocity = -250.0f;

    public AnimatedSprite2D sprite => GetNode<AnimatedSprite2D>("AnimatedSprite2D");

    public override void _Ready()
    {
        StatusLabel = new Label();

        // Initialize states
        StateManager.AddState("Idle", new PlayerIdleState(this));
        StateManager.AddState("Walking", new PlayerWalkingState(this));
        StateManager.AddState("Running", new PlayerRunningState(this));
        StateManager.AddState("Jumping", new PlayerJumpingState(this));

        // Set initial state
        StateManager.ChangeState("Idle");

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
        // Add gravity if not on the floor
        if (!IsOnFloor())
            velocity.Y += Gravity * (float)delta;

        // Handle input and update the current state
        StateManager.HandleInput();
        StateManager.Update(delta);

        // Move the character
        Velocity = velocity;
        MoveAndSlide();
        StatusLabel.Text = StateManager.GetCurrentState().ToString();
    }
}
