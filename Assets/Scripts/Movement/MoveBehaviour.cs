using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour, IMoveBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D rigidBody;

    public void Init(float speed, float jumpForce)
    {
        this.speed = speed;
        this.jumpForce = jumpForce;
        if (rigidBody == null)
            rigidBody = transform.GetComponent<Rigidbody2D>();
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
    }

    public void SetVertical(float y)
    {
        rigidBody.AddForce(Vector2.up * y * jumpForce);
    }
}
