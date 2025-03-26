using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[Serializable]
public struct EnemyGroup
{
    public EnemyStats enemyType;
    public int amount;
    public float spacing;
}


[CreateAssetMenu(fileName = "Wave", menuName = "Wave/Wave")]
public class Wave : ScriptableObject
{
    public List<EnemyGroup> waveGroups;
}
