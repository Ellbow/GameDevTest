using System.Collections.Generic;

public class StateManager
{
    private IState currentState;
    private string currentStateName;
    private Dictionary<string, IState> states = new Dictionary<string, IState>();

    public void AddState(string name, IState state)
    {
        states[name] = state;
    }

    public void ChangeState(string name)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = states[name];
        currentStateName = name;
        currentState.Enter();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update(double delta)
    {
        currentState?.Update(delta);
    }

    public string GetCurrentState()
    {
        return currentStateName;
    }
}
