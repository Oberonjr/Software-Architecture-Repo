using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The state when building is enabled on the grid
public class BuildState : IGameState
{
    StateMachine stateMachine;

    public BuildState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void EnterState()
    {
        EventBus<StartBuildPhaseEvent>.Publish(new StartBuildPhaseEvent());
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        
    }
}
