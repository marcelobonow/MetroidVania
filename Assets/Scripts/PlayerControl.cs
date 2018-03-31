using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [SerializeField] private MovableBehaviour behaviour;

    [SerializeField] float speed;
    private float horizontalInput;

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
        if(Mathf.Abs(horizontalInput) > 0.02)
        {
            behaviour.Move(horizontalInput * speed);
        }
        

	}
}
