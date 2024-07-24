using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine<T> : Node where T : Node
{
    public enum State
    {
        Idle,
        Walking,
        Running,
        Jumping
    }

    private State currentState = State.Idle;
    private T owner;

    public StateMachine(T owner)
    {
        this.owner = owner;
    }

    public State CurrentState => currentState;

    public void SetState(State newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case State.Idle:
                owner.Call("OnEnterIdle");
                break;
            case State.Walking:
                owner.Call("OnEnterWalking");
                break;
            case State.Running:
                owner.Call("OnEnterRunning");
                break;
            case State.Jumping:
                owner.Call("OnEnterJumping");
                break;
        }
    }

    public void TransitionToState(State newState)
    {
        if (currentState != newState)
        {
            SetState(newState);
        }
    }
}
