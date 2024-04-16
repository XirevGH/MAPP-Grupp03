using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform pos;
    private Rigidbody2D rb;
    public DynamicJoystick dynamicJoystick;

    private SpriteRenderer rend;
    Vector2 axisInput;
    private void Awake()
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
        if (dynamicJoystick.Horizontal < 0f)
        {
            FlipSprite(true);
        }
        if (dynamicJoystick.Horizontal > 0f)
        {
            FlipSprite(false);
        }
    }

    private void FlipSprite(bool flip)
    {
        rend.flipX = flip; 
    }

    public void FixedUpdate()
    {
        Debug.Log(dynamicJoystick.Horizontal);
        pos.position = new Vector2(pos.position.x + (dynamicJoystick.Horizontal / 100) * moveSpeed, pos.position.y + (dynamicJoystick.Vertical / 100) * moveSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(axisInput);
        axisInput = context.ReadValue<Vector2>();
    }

}










