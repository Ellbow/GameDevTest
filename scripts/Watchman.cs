using Godot;
using GodotStateCharts;
using System;

public partial class Watchman : Node2D
{
    private Enemy enemy = null;
    public int enemyEnteredCount = 0;

    private StateChart stateChart;

    // Assuming the Area2D node is a direct child of this Watchman node
    private Area2D area => GetNode<Area2D>("Area2D");

    public override void _Ready()
    {
        // ⚠️ Note: The initial state of a state chart will only be entered one frame after the state chart's _ready function ran. 
        // It is done this way to give nodes above the state chart time to run their _ready functions before any state chart logic is triggered.
        // This means that if you call send_event, set_expression_property or step in a _ready function things will most likely not work as expected. 
        // If you must call any of these functions in a _ready function, you can use call_deferred to delay the event sending by one frame, e.g. state_chart.send_event.call_deferred("some_event").
        
        area.AreaEntered += OnAreaEntered2D;
        area.AreaExited += OnAreaExited2D;
        stateChart = StateChart.Of(GetNode("StateChart"));

        PrintTree(GetTree().Root);

        //var stateIdle = StateChartState.Of(GetNode("StateChart/Root/AlertState/Idle"));
        //stateEnt.Connect(StateChartState.SignalName.StateEntered, Callable.From(OnStateEntered));
                
        //var stateObserving = StateChartState.Of(GetNode("StateChart/Root/AlertState/Observing"));
        //state.Connect(StateChartState.SignalName.StateProcessing, Callable.From((float delta) => OnObservingStateProcessing(delta)));
    }

    private void OnAreaEntered2D(Area2D area)
    {
        enemyEnteredCount++;
        stateChart.SetExpressionProperty("enemyEnteredCount", enemyEnteredCount);
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
    
    private void OnNormalStateEntered()
    {
        GetNode<Sprite2D>("Sprite2D").Modulate = new Color(1,1,1);
        enemyEnteredCount = 0;
        stateChart.SetExpressionProperty("enemyEnteredCount", enemyEnteredCount);
    }

    private void OnBerserkStateEntered()
    {
        GetNode<Sprite2D>("Sprite2D").Modulate = new Color(1, 0, 0);
    }

    private void PrintTree(Node node, int indent = 0)
    {
        GD.Print(new string(' ', indent * 2) + node.Name);
        foreach (Node child in node.GetChildren())
        {
            PrintTree(child, indent + 1);
        }
    }
}
