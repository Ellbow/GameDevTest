using Godot;

public class PlayerRunningState : IState
{
    private player player;

    public PlayerRunningState(player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.sprite.Play("run");
        GD.Print("Entering running State");
    }

    public void Exit()
    {
        // Exit logic
        GD.Print("Exiting running State");
    }

    public void HandleInput()
    {
        float direction = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        player.velocity.X = direction * player.RunSpeed;

        if (direction == 0 && player.IsOnFloor())
        {
            player.StateManager.ChangeState("Idle");
        }

        if (!Input.IsActionPressed("Sprint"))
        {
            player.StateManager.ChangeState("Walking");
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