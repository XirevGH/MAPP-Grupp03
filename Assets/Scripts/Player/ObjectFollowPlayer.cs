using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowPlayer : MonoBehaviour
{
    public float followSpeed = 6f;
    public float yOffset = 1f;
    private Player target;

    private void Awake()
    {
        target = FindObjectOfType<Player>();
    }
    void FixedUpdate()
    {

        Vector3 newPos = new Vector3(target.transform.position.x, target.transform.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
