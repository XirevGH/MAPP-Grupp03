using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, Input_Actions.IPlayerActions
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform pos;
    private Rigidbody2D rb;
    private float horizontalValue;
    private float verticalValue;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>(); 
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontalValue, verticalValue);
        if (inputVector.magnitude > 1)
        {
            inputVector.Normalize();
            horizontalValue = inputVector.x;
            verticalValue = inputVector.y;
            
        }
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
        rend.flipX = flip; 
    }

    void FixedUpdate()
    {
        pos.position = new Vector2(pos.position.x + horizontalValue * moveSpeed * Time.deltaTime, pos.position.y + verticalValue * moveSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 axisInput = context.ReadValue<Vector2>();
        rb.velocity = new Vector2(axisInput.x * moveSpeed, axisInput.y * moveSpeed);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}










