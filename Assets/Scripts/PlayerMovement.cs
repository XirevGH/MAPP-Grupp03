using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform pos;
    public DynamicJoystick dynamicJoystick;

    private float horizontalValue;
    private float verticalValue;

    private SpriteRenderer rend;
    private Animator anim;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>(); 
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");

        anim.SetFloat("MoveSpeed", Mathf.Abs(dynamicJoystick.Horizontal + dynamicJoystick.Vertical / 2));
        Vector2 inputVector = new Vector2(horizontalValue, verticalValue);
        if (inputVector.magnitude > 1)
        {
            inputVector.Normalize();
            horizontalValue = inputVector.x;
            verticalValue = inputVector.y;

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
        if (dynamicJoystick.Horizontal != 0f || dynamicJoystick.Vertical != 0f)
        {
            pos.position = new Vector2(pos.position.x + (dynamicJoystick.Horizontal / 100) * moveSpeed, pos.position.y + (dynamicJoystick.Vertical / 100) * moveSpeed);
        }
        else
        {
            pos.position = new Vector2(pos.position.x + horizontalValue * moveSpeed, pos.position.y + verticalValue * moveSpeed);
        }

    }
}