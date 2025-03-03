using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttacker : ScriptableObject
{
    public abstract void Attack(Transform pSource, TowerStats tower, List<GameObject> pTargetPosition = null);

}
