using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private void Update()
    {
        var x = Math.Round(Input.GetAxis("Horizontal"), 3);
        var y = Math.Round(Input.GetAxis("Vertical"), 3);

        if (x != 0)
            EventManager.OnEvent(this, x, Events.HORIZONTAL_INPUT);
        if (y != 0)
            EventManager.OnEvent(this, y, Events.VERTICAL_INPUT);
    }

}
