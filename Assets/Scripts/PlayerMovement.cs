using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform pos;
    public DynamicJoystick dynamicJoystick;

    private SpriteRenderer rend;
    Vector2 axisInput;
    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>(); 
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

    void FixedUpdate()
    {
        pos.position = new Vector2(pos.position.x + (dynamicJoystick.Horizontal / 100) * moveSpeed, pos.position.y + (dynamicJoystick.Vertical / 100) * moveSpeed);
    }
}










