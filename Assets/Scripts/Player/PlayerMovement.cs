using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private DynamicJoystick dynamicJoystick;
    private float horizontalValue;
    private float verticalValue;

    private float movementSpeedDecrease = 1f;

    private SpriteRenderer rend;
    private Animator anim;

    public bool isSlowed;

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

    public void IncreaseMovementSpeed(float movementSpeedIncrease)
    {
        movementSpeed *= movementSpeedIncrease;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
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

        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (dynamicJoystick == null)
            {
                
                DynamicJoystick[] dynamicJoysticks = FindObjectsOfType<DynamicJoystick>();
                foreach (DynamicJoystick joystick in dynamicJoysticks)
                {
                    if (joystick.GetPosition() == "Left")
                    {
                        dynamicJoystick = joystick;
                    }
                }
            }
            if (Player.Instance.PlayerIsAlive()) { 
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
                    transform.position = new Vector2(transform.position.x + dynamicJoystick.Horizontal * movementSpeed * movementSpeedDecrease * Time.deltaTime, transform.position.y + dynamicJoystick.Vertical * movementSpeed * movementSpeedDecrease* Time.deltaTime);
                }
                else
                {
                    anim.SetFloat("MoveSpeed", Mathf.Abs(horizontalValue + verticalValue / 2));
                    transform.position = new Vector2(transform.position.x + horizontalValue * movementSpeed * movementSpeedDecrease * Time.deltaTime, transform.position.y + verticalValue * movementSpeed * movementSpeedDecrease * Time.deltaTime);
                }
            }
            else
            {
                anim.SetFloat("MoveSpeed", 0);
            }
        }
    }

    public void DecreaseMovementSpeed(float percentageDecrease)
    {   
        movementSpeedDecrease = percentageDecrease;
    }
}