using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, Input_Actions.IPlayerActions
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform pos;
    private Rigidbody2D rb;

    Input_Actions playerInputActions;


    private SpriteRenderer rend;
    Vector2 axisInput;
    private void Awake()
    {
        playerInputActions = new Input_Actions();
        rend = GetComponent<SpriteRenderer>(); 
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
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

    void FixedUpdate()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Hello");
        axisInput = context.ReadValue<Vector2>();
        pos.position = new Vector2(pos.position.x + axisInput.x * moveSpeed, pos.position.y + axisInput.y * moveSpeed);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}










