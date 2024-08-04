using Godot;
using System;

public interface IState
{
    void Enter();
    void Exit();
    void HandleInput();
    void Update(double delta);
}
