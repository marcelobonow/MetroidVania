using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [SerializeField] private MovableBehaviour behaviour;

    [SerializeField] float speed;
    private float horizontalInput;
    private float jumpInput;

    private void Awake()
    {
        if(behaviour == null && (behaviour = GetComponent<MovableBehaviour>()) == null)
        {
            Debug.LogError("Nao tem movableBehaviour");
            Application.Quit();
        }
    }

    void Update () {

        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");
        if (Mathf.Abs(horizontalInput) > 0.02)
        {
            GetComponent<Rigidbody2D>().MovePosition(new Vector3((transform.position.x + horizontalInput * speed), transform.position.y, 0));
            //behaviour.Move(horizontalInput * speed);
        }
        if(jumpInput > 0)
        {
            RaycastHit info;
            if(Physics.Raycast(transform.position, Vector3.down, out info, 0.7f))
            {
                GetComponent<Rigidbody2D>().AddForce(Vector3.up * 100); 
            }
        }
	}
}
