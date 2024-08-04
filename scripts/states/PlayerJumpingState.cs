    using Godot;

    public class PlayerJumpingState : IState
    {
        private player player;

        public PlayerJumpingState(player player)
        {
            this.player = player;
        }

        public void Enter()
        {
            player.sprite.Play("jumping");
            player.velocity.Y = player.JumpVelocity;
            GD.Print("Entering Jumping State");
        }

        public void Exit()
        {
            // Exit logic
            GD.Print("Exiting Jumping State");
        }

        public void HandleInput()
        {
            // if (player.IsOnFloor())
            // {
            //     player.StateManager.ChangeState("Idle");
            // }
        }

        public void Update(double delta)
        {
            float direction = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
            player.velocity.X = direction * player.WalkSpeed;

            if (player.IsOnFloor() && player.velocity.Y > 0)
            {
                player.StateManager.ChangeState("Idle");
            }
        }
    }