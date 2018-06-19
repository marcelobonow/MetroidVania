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
        float rayXPosition = x > 0 ? collider.bounds.min.x : collider.bounds.max.x;
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
            float colliderHitDifference = RayCastHitInfo.transform.position.x - (x > 0 ? - RayCastHitInfo.collider.bounds.max.x : RayCastHitInfo.collider.bounds.min.x);
            float playerColliderDifference = transform.position.x - (x > 0 ? - collider.bounds.max.x : collider.bounds.min.x);
            float dumpPosition = RayCastHitInfo.transform.position.x + (colliderHitDifference + playerColliderDifference)+0.2f ;
            transform.position = new Vector2(dumpPosition, transform.position.y + y);
        }
       
    }

    public void Move(float x)
    {
        MoveAdd(x, 0);
    }

}