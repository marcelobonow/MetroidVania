using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour, IMoveBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject playerBase;

    [SerializeField] private float turnThereshold;

    public void Init(float speed, float jumpSpeed)
    {
        this.speed = speed;
        this.jumpSpeed = jumpSpeed;

    }

    public void SetSpeed(float speed)
    {
        if (speed < 0)
            speed = 0;
        else
            this.speed = speed;
    }

    public void SetHorizontal(float x)
    {
        rigidBody.velocity = new Vector2(x * speed, rigidBody.velocity.y);

        Vector3 oldRotation = playerBase.transform.rotation.eulerAngles;
        if (x < -turnThereshold)
            oldRotation.y = 180;

        else if (x > turnThereshold)
            oldRotation.y = 0;
        playerBase.transform.rotation = Quaternion.Euler(oldRotation);

        playerAnimator.SetFloat("Speed", Mathf.Abs(x));
    }
    public void SetVertical(float y)
    {
        //rigidBody.AddForce(Vector2.up * y * jumpForce);
    }
    public void Jump()
    {
        ///TODO: Jump enquanto tiver pressionando o botão
        RaycastHit2D rayCastInfo = Physics2D.Raycast(playerBase.transform.position, Vector2.down, 4f);
        if (rayCastInfo.collider != null)
        {
            if (rayCastInfo.collider.gameObject.tag == "Ground")
            {
                Debug.Log("Gonna Jump");
                rigidBody.velocity = Vector2.up * jumpSpeed;
            }
        }
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
}
