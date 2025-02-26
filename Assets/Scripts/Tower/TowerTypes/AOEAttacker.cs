using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOEAttacker", menuName = "Tower Types/AOE Attacker")]
public class AOEAttacker : AbstractAttacker
{
    public override void Attack(Transform pSource, TowerStats tower, List<Vector3> pTargetPositions)
    {
        throw new System.NotImplementedException();
    }
}
