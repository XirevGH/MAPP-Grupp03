using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowTarget : MonoBehaviour
{
    public float followSpeed = 6f;
    public float yOffset = 1f;
    public GameObject target;

    private void Awake()
    {
        if (target == null)
        {
            target = FindObjectOfType<Player>().gameObject;
        }
    }
    void FixedUpdate()
    {

        Vector3 newPos = new Vector3(target.transform.position.x, target.transform.position.y + yOffset, transform.position.z);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
