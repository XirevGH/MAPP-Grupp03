using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform pos;

    private float horizontalValue;
    private float verticalValue;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");

        /*Vector2 inputVector = new Vector2(horizontalValue, verticalValue);
        if (inputVector.magnitude > 1)
        {
            inputVector.Normalize();
            horizontalValue = inputVector.x;
            verticalValue = inputVector.y;
            
        }*/
        if(horizontalValue < 0f)
        {
            FlipSprite(true);
        }
        if (horizontalValue > 0f)
        {
            FlipSprite(false);
        }
    }

    private void FlipSprite(bool flip)
    {
        if (flip) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        pos.position = new Vector2(pos.position.x + horizontalValue * moveSpeed * Time.deltaTime, pos.position.y + verticalValue * moveSpeed * Time.deltaTime);
    }
}










