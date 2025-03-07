using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndTowerSelection();
        }
    }


    public void EndTowerSelection()
    {
        EventBus<DeselectTowerEvent>.Publish(new DeselectTowerEvent());
    }
}
