using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private DynamicJoystick dynamicJoystick;

    private float movementSpeedDecrease = 1f;
    private SpriteRenderer rend;
    private Animator anim;

    public bool isSlowed;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (dynamicJoystick == null)
        {
            DynamicJoystick[] dynamicJoysticks = FindObjectsOfType<DynamicJoystick>();
            foreach (DynamicJoystick joystick in dynamicJoysticks)
            {
                if (joystick.GetPosition() == "Left")
                {
                    dynamicJoystick = joystick;
                    break;
                }
            }
        }
    }

    public void FixedUpdate()
    {
        if (Time.timeScale == 0f || !Player.Instance.PlayerIsAlive())
        {
            if (anim != null) anim.SetFloat("MoveSpeed", 0f);
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 inputDir = new Vector2(h, v);

        if (inputDir.magnitude > 1f)
        {
            inputDir.Normalize();
        }

        Vector2 moveDirection = Vector2.zero;

        if (dynamicJoystick != null && (dynamicJoystick.Horizontal != 0f || dynamicJoystick.Vertical != 0f))
        {
            moveDirection = new Vector2(dynamicJoystick.Horizontal, dynamicJoystick.Vertical);
        }
        else if (inputDir != Vector2.zero)
        {
            moveDirection = inputDir;
        }

        if (moveDirection.x < 0f) FlipSprite(true);
        else if (moveDirection.x > 0f) FlipSprite(false);

        if (moveDirection != Vector2.zero)
        {
            float currentSpeed = movementSpeed * movementSpeedDecrease;
            transform.position += (Vector3)(moveDirection * currentSpeed * Time.fixedDeltaTime);

            if (anim != null)
            {
                anim.SetFloat("MoveSpeed", moveDirection.magnitude);
            }
        }
        else
        {
            if (anim != null)
            {
                anim.SetFloat("MoveSpeed", 0f);
            }
        }
    }

    private void FlipSprite(bool flip)
    {
        if (rend != null) rend.flipX = flip;
    }

    public void IncreaseMovementSpeed(float multiplier)
    {
        movementSpeed *= multiplier;
    }

    public float GetMovementSpeed() => movementSpeed;

    public void DecreaseMovementSpeed(float multiplier)
    {
        movementSpeedDecrease = multiplier;
    }
}