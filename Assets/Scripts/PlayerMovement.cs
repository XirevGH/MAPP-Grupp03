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


    private SpriteRenderer rend;
    Vector2 axisInput;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>(); 
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        Debug.Log(axisInput.x);
        if (Time.timeScale == 0f)
        {
            return;
        }
        if (axisInput.x < 0f)
        {
            FlipSprite(true);
        }
        if (axisInput.x > 0f)
        {
            FlipSprite(false);
        }
    }

    private void FlipSprite(bool flip)
    {
        rend.flipX = flip; 
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        axisInput = context.ReadValue<Vector2>();
        rb.velocity = new Vector2(axisInput.x * moveSpeed, axisInput.y * moveSpeed);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}










