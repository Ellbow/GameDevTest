using Godot;

public class PlayerIdleState : IState
{
    private player player;

    public PlayerIdleState(player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        GD.Print("Entering Idle State");
        player.sprite.Play("idle");
    }

    public void Exit()
    {
        GD.Print("Exiting Idle State");
    }

    public void HandleInput()
    {
        if (Input.IsActionPressed("move_right") || Input.IsActionPressed("move_left"))
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
        // Idle update logic
        // if (player.IsOnFloor())
        // {
        //     if (player.direc .X == 0)
        //     {
        //         player.StateManager.ChangeState("Idle");
        //     }

            
        // }
    }
}
