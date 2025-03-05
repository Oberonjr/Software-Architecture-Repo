using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicatorManager : MonoBehaviour
{
   [SerializeField] Transform rangeIndicator;


   public void ToggleRangeIndicator(TowerController tc)
   {
      rangeIndicator.gameObject.SetActive(!rangeIndicator.gameObject.activeSelf);
      rangeIndicator.localScale = new Vector3(tc.TowerStats.stats.towerRange * 2, tc.TowerStats.stats.towerRange * 2, 1);
   }

   
}
