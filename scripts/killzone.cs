using Godot;
using System;

public partial class killzone : Area2D
{
    public override void _Ready()
	{
		BodyEntered += OnBodyEntered;		
	}

    private async void OnBodyEntered(Node body)
	{
        GD.Print("You died");
        //Creates a RefCounted timer (different from a node timer) that sends a timeout signal on completion. asynchronous ToSignal will await completion then move to next line 
        await ToSignal(GetTree().CreateTimer(0.6f), SceneTreeTimer.SignalName.Timeout);
        //The timer will be dereferenced after its time elapses, as SceneTreeTimer will be collect by GC because it inherits from RefCounted
        GetTree().ReloadCurrentScene();
	}
}
