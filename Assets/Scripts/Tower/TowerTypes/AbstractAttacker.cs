using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Parent container for attack method
//ScriptableObject for Strategy principle, allowing for easy switches for different tower behaviour
public abstract class AbstractAttacker : ScriptableObject
{
    public abstract void Attack(Transform pSource, TowerStats tower, List<GameObject> pTargetPosition = null);

}
