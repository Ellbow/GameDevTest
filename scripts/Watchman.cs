using Godot;
using GodotStateCharts;
using System;

public partial class Watchman : Node2D
{
    Enemy enemy = null;

    private StateChart stateChart;

    // Assuming the Area2D node is a direct child of this Watchman node
    private Area2D area => GetNode<Area2D>("Area2D");

    public override void _Ready()
    {
        area.AreaEntered += OnAreaEntered2D;
        area.AreaExited += OnAreaExited2D;
        stateChart = StateChart.Of(GetNode("StateChart"));

        // get the active state node
        var stateEnt = StateChartState.Of(GetNode("%Idle"));
        // connect to the state_entered signal
        stateEnt.Connect(StateChartState.SignalName.StateEntered, Callable.From(OnStateEntered));

        // += OnStateEntered;

        // var state = StateChartState.Of(GetNode("%Observing"));
        // // connect to the state_entered signal
        // state.Connect(StateChartState.SignalName.StateProcessing, Callable.From(OnObservingStateProcessing));
                
        var state = StateChartState.Of(GetNode("%Observing"));
        //state.Connect(StateChartState.SignalName.StateProcessing, new Callable(this, nameof(OnObservingStateProcessing)));
        state.Connect(StateChartState.SignalName.StateProcessing, Callable.From((float delta) => OnObservingStateProcessing(delta)));
    }
    

    private void OnAreaEntered2D(Area2D area)
    {
        stateChart.SendEvent("EnemyEntered");
        enemy = area.GetParent() as Enemy;
    }

    private void OnAreaExited2D(Area2D area)
    {
        stateChart.SendEvent("EnemyExited");
    }

    private void OnObservingStateProcessing(float delta)
    {
        LookAt(enemy.Position);
    }

    private void OnStateEntered()
    {
        RotationDegrees = -90;
        enemy = null;
    }
}
