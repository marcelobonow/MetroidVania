using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBehaviour : MonoBehaviour {


    public LayerMask layersToIgnoreCollision;
    private new Collider collider;

    private void Awake()
    {
        if(collider == null)
            collider = GetComponent<Collider>();
    }

    public void MoveAdd(float x, float y)
    {
        float rayXPosition = x > 0 ? collider.bounds.max.x : collider.bounds.min.x;
        Ray ray = new Ray(new Vector3(rayXPosition, transform.position.y), new Vector3(x, y, 0));
        float maxDistance = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
        RaycastHit RayCastHitInfo;
        int layers = int.MaxValue - layersToIgnoreCollision.value;
        if(!Physics.Raycast(ray, out RayCastHitInfo, maxDistance, layers))
        {
            Debug.DrawLine(transform.position, new Vector3(rayXPosition+ x, transform.position.y + y, 0), Color.red, 0.5f);
            transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
        }
        else
        {
            float dumpPosition = rayXPosition - (rayXPosition - transform.position.x);
            transform.position = new Vector2(dumpPosition, transform.position.y + y);
        }
       
    }

    public void Move(float x)
    {
        MoveAdd(x, 0);
    }

}
