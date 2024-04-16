using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveToPlayer : MonoBehaviour
{
    private Transform target;
    private bool move = false;
    public float speed = 20f;

    

    public void StartMoving(Transform playerTransform)
    {
        
        target = playerTransform;
        move = true;
        
        
    }

    void FixedUpdate()
    {
        if (move && target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}

