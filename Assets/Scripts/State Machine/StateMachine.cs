using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IGameState CurrentState { get; private set; }

    public void ChangeState(IGameState newState)
    {
        CurrentState?.ExitState();
        CurrentState = newState;
        CurrentState?.EnterState();
    }

    public void Update()
    {
        CurrentState?.UpdateState();
    }
}
