using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private CharacterMovementData playerMovementData;
    [SerializeField] private IMoveBehaviour moveBehaviour;


    private void Awake()
    {
        if (moveBehaviour == null)
            moveBehaviour = gameObject.AddComponent<MoveBehaviour>();
        else
            moveBehaviour = GetComponent<MoveBehaviour>();
        moveBehaviour.Init(playerMovementData);
        EventManager.AddListener(Events.HORIZONTAL_INPUT, OnHorizontalInput);
        EventManager.AddListener(Events.VERTICAL_INPUT, OnVerticalInput);
        EventManager.AddListener(Events.JUMP_INPUT, OnJumpInput);
        EventManager.AddListener(Events.PLAYER_IN_AIR, OnPlayerInAir);
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
    ///TODO: Passar para a camera
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
    private void OnPlayerInAir(object sender, object verticalSpeed)
    {
        if (verticalSpeed is double)
        {
            var input = (double)verticalSpeed;
            Debug.Log("Vertical Speed: " + verticalSpeed);
        }
    }
}
