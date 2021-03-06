﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour, IMoveBehaviour
{
    private CharacterMovementData movementData;

    private GameObject playerBase;
    private Rigidbody2D rigidBody;
    private Animator playerAnimator;

    private bool crunch = false;
    private bool inAir = false;

    public void Init(CharacterMovementData movementData)
    {
        this.movementData = movementData;

        rigidBody.gravityScale = movementData.fallSpeed;
    }

    private void Awake()
    {
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody2D>();
        if (playerAnimator == null)
            playerAnimator = GetComponent<Animator>();
        if (playerBase == null)
            playerBase = gameObject;

        EventManager.AddListener(Events.PLAYER_IN_AIR, OnAir);
    }

    private void Update()
    {
        EventManager.OnEvent(this, rigidBody.velocity.y, Events.PLAYER_IN_AIR);
        inAir = Math.Abs(rigidBody.velocity.y) > 0;
    }

    public void SetHorizontal(float x)
    {
        rigidBody.velocity = new Vector2(x * movementData.speed, rigidBody.velocity.y);

        Vector3 oldRotation = playerBase.transform.rotation.eulerAngles;
        if (x < -movementData.turnPoint)
            oldRotation.y = 180;

        else if (x > movementData.turnPoint)
            oldRotation.y = 0;
        playerBase.transform.rotation = Quaternion.Euler(oldRotation);

        playerAnimator.SetFloat("HorizontalSpeed", Mathf.Abs(x));
    }
    public void SetVertical(float y)
    {
        if (!crunch && y < 0 && !inAir)
        {
            crunch = true;
            playerAnimator.SetBool("Crunch", true);
        }
        if (crunch && y >= 0 && !inAir)
        {
            crunch = false;
            playerAnimator.SetBool("Crunch", false);
        }

    }

    public void Jump()
    {
        ///TODO: Jump enquanto tiver pressionando o botão
        ///TODO: Verificar algo para não testar todo frame
        RaycastHit2D rayCastInfo = Physics2D.Raycast(playerBase.transform.position, Vector2.down, 1f);
        if (rayCastInfo.collider != null)
        {
            if (rayCastInfo.collider.gameObject.tag == "Ground")
            {
                rigidBody.velocity = Vector2.up * movementData.jumpSpeed;
                Debug.LogWarning("Pulo  ao tempo: " + Time.time);
            }
        }
    }

    private void OnAir(object sender, object verticalSpeed)
    {
        if (verticalSpeed is float)
        {
            var speed = (float)verticalSpeed;
            playerAnimator.SetFloat("VerticalSpeed", (float)speed);
            if (speed < 0 || (!InputManager.IsJumpPressed() && speed > 0))
                rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (movementData.fallMultiplier - 1) * movementData.fallSpeed * Time.deltaTime;
        }
    }
}
