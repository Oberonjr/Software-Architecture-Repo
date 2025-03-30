using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * StateMachine responsible for changing between combat and build state
 * Ensures state transitioning is done correctly
 * Dependent on being defined in the GameManager at the start
 * and it's own update being mirrored in a MonoBehaviour update
 */
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
