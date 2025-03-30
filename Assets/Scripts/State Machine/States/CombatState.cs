using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State in which building is disabled and enemies spawn until exhausted
public class CombatState : IGameState
{
    StateMachine stateMachine;
    

    public CombatState(StateMachine pStateMachine)
    {
        stateMachine = pStateMachine;
        
    }

    public void EnterState()
    {
        EventBus<StartWaveEvent>.Publish(new StartWaveEvent());
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        EventBus<EndWaveEvent>.Publish(new EndWaveEvent(WaveManager.Instance.CurrentWaveIndex));
    }
}
