using Godot;
using System;

public partial class Enemy : Node2D
{
    public override void _Process(double delta)
    {        
        // Move the enemy towards the mouse position
        Position = GetGlobalMousePosition();
    }
}