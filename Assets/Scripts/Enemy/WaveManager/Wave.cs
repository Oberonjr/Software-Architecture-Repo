using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Struct that dictates the way a specific group is spawned
 * EnemyType dictates which type of enemy will be spawned for this group
 * Amount is the amount of enemies in this group
 * Spacing is the amount of time between each enemy spawn in this group
 */
[Serializable]
public struct EnemyGroup
{
    public EnemyStats enemyType;
    public int amount;
    public float spacing;
}

// Container ScriptableObject that determines the constituents of each Wave
[CreateAssetMenu(fileName = "Wave", menuName = "Wave/Wave")]
public class Wave : ScriptableObject
{
    public List<EnemyGroup> waveGroups;
}
