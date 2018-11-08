using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        var x = Math.Round(Input.GetAxis("Horizontal"), 4);
        var y = Math.Round(Input.GetAxisRaw("Vertical"), 4);
        var jumpDown = Input.GetButtonDown("Jump");


        EventManager.OnEvent(this, x, Events.HORIZONTAL_INPUT);
        EventManager.OnEvent(this, y, Events.VERTICAL_INPUT);
        if (jumpDown)
            EventManager.OnEvent(this, null, Events.JUMP_INPUT);
    }

    public static bool IsJumpPressed()
    {
        return Input.GetButton("Jump");
    }
}
