using Godot;

public class PlayerWalkingState : IState
{
    private player player;

    public PlayerWalkingState(player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.sprite.Play("run");
        GD.Print("Entering walking State");
    }

    public void Exit()
    {
        // Exit logic
        GD.Print("Exiting walking State");
    }

    public void HandleInput()
    {
        float direction = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        player.velocity.X = direction * player.WalkSpeed;

        if (direction == 0 && player.IsOnFloor())
        {
            player.StateManager.ChangeState("Idle");
        }

        if (Input.IsActionPressed("Sprint"))
        {
            player.StateManager.ChangeState("Running");
        }

        if (Input.IsActionJustPressed("jump"))
        {
            player.StateManager.ChangeState("Jumping");
        }
    }

    public void Update(double delta)
    {
        // Update logic
    }
}
