using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * General interface as the parent for states
 * Implemented as interface to ensure all states have the three main functions
 * Mostly done to force myself to code the states properly
 * even though they are underutilized
 */
public interface IGameState
{
    void EnterState();
    void UpdateState();
    void ExitState();
}
