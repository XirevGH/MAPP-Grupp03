using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private DynamicJoystick leftJoystick;
    [SerializeField] private DynamicJoystick rightJoystick;

    public static JoystickController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleJoysticks(bool state)
    {
        if (leftJoystick != null)
        {
            leftJoystick.gameObject.SetActive(state);
            leftJoystick.handle.anchoredPosition = Vector2.zero;
            leftJoystick.input = Vector2.zero;
        }

        if (rightJoystick != null)
        {
            rightJoystick.gameObject.SetActive(state);
            rightJoystick.handle.anchoredPosition = Vector2.zero;
            rightJoystick.input = Vector2.zero;
        }
    }
}