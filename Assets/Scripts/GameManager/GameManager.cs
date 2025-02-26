using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isSpedUp;
    
    public void ToggleGameSpeed()
    {
        isSpedUp = !isSpedUp;
        Time.timeScale = isSpedUp ? 3 : 1;
    }
}
