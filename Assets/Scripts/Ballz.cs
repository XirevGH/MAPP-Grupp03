using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Ballz : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform endPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPoint.position, moveSpeed * Time.deltaTime);
    }
   

    private void Update()
    {
        if (this.transform.position == endPoint.position)
        {
            Debug.Log("Game Over?");

        }
    }

}
