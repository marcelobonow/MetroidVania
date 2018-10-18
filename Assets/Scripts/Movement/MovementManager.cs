using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    [SerializeField] private float jumpSpeed = 20;
    [SerializeField] private IMoveBehaviour moveBehaviour;


    private void Awake()
    {
        if (moveBehaviour == null)
        {
            moveBehaviour = gameObject.AddComponent<MoveBehaviour>();
            moveBehaviour.Init(speed, jumpSpeed);
        }
        moveBehaviour = GetComponent<MoveBehaviour>();
        EventManager.AddListener(Events.HORIZONTAL_INPUT, OnHorizontalInput);
        EventManager.AddListener(Events.VERTICAL_INPUT, OnVerticalInput);
        EventManager.AddListener(Events.JUMP_INPUT, OnJumpInput);
    }

    private void OnHorizontalInput(object sender, object horizontalInput)
    {
        if (horizontalInput is double)
        {
            var input = (double)horizontalInput;
            Debug.Log("Horizontal: " + input);
            moveBehaviour.SetHorizontal((float)input);
        }
    }
    private void OnVerticalInput(object sender, object verticalInput)
    {
        if (verticalInput is double)
        {
            var input = (double)verticalInput;
            Debug.Log("Vertical: " + input);
            moveBehaviour.SetVertical((float)input);
        }
    }
    private void OnJumpInput(object Sender, object jumpInput)
    {
        Debug.Log("Jump");
        moveBehaviour.Jump();
    }
}
