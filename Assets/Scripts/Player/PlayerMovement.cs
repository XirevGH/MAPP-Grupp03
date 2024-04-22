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

      


    }

    private void FlipSprite(bool flip)
    {
        rend.flipX = flip; 
    }

    public void FixedUpdate()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        verticalValue = Input.GetAxisRaw("Vertical");
        Vector2 inputVector = new Vector2(horizontalValue, verticalValue);
        if (inputVector.magnitude > 1)
        {
            inputVector.Normalize();
            horizontalValue = inputVector.x;
            verticalValue = inputVector.y;

        }

        if (dynamicJoystick.Horizontal < 0f || horizontalValue < 0f)
        {
            FlipSprite(true);
        }
        if (dynamicJoystick.Horizontal > 0f || horizontalValue > 0f)
        {
            FlipSprite(false);
        }
        

        if (dynamicJoystick.Horizontal != 0f || dynamicJoystick.Vertical != 0f)
        {
            anim.SetFloat("MoveSpeed", Mathf.Abs(dynamicJoystick.Horizontal + dynamicJoystick.Vertical / 2));
            pos.position = new Vector2(pos.position.x + dynamicJoystick.Horizontal * moveSpeed * Time.deltaTime, pos.position.y + dynamicJoystick.Vertical * moveSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetFloat("MoveSpeed", Mathf.Abs(horizontalValue + verticalValue / 2));
            pos.position = new Vector2(pos.position.x + horizontalValue * moveSpeed * Time.deltaTime, pos.position.y + verticalValue * moveSpeed * Time.deltaTime);
        }

    }
}