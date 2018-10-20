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
    }

    public void SetHorizontal(float x)
    {
        rigidBody.velocity = new Vector2(x * movementData.speed, rigidBody.velocity.y);

        Vector3 oldRotation = playerBase.transform.rotation.eulerAngles;
        if (x < -turnThereshold)
            oldRotation.y = 180;

        else if (x > turnThereshold)
            oldRotation.y = 0;
        playerBase.transform.rotation = Quaternion.Euler(oldRotation);

        playerAnimator.SetFloat("Speed", Mathf.Abs(x));
    }

    public void Jump()
    {
        ///TODO: Jump enquanto tiver pressionando o botão
        RaycastHit2D rayCastInfo = Physics2D.Raycast(playerBase.transform.position, Vector2.down, 1f);
        if (rayCastInfo.collider != null)
        {
            if (rayCastInfo.collider.gameObject.tag == "Ground")
            {
                rigidBody.velocity = Vector2.up * movementData.jumpSpeed;
            }
        }
    }


}
