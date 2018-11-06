using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour, IMoveBehaviour
{

    ///TODO: Marcelo: passar para classe de controle?
    [SerializeField] private float turnThereshold;

    private CharacterMovementData movementData;

    private GameObject playerBase;
    private Rigidbody2D rigidBody;
    private Animator playerAnimator;

    private bool crunch = false;
    private bool inAir = false;

    public void Init(CharacterMovementData movementData)
    {
        this.movementData = movementData;
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
        var movementSpeed = crunch ? movementData.cruchSpeed : movementData.speed;
        rigidBody.velocity = new Vector2(x * movementSpeed, rigidBody.velocity.y);

        Vector3 oldRotation = playerBase.transform.rotation.eulerAngles;
        if (x < -turnThereshold)
            oldRotation.y = 180;

        else if (x > turnThereshold)
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
            }
        }
    }

    private void OnAir(object sender, object verticalSpeed)
    {
        if (verticalSpeed is float)
        {
            var speed = (float)verticalSpeed;
            playerAnimator.SetFloat("VerticalSpeed", (float)speed);
        }
    }
}
