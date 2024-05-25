using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public static JoystickController Instance
    {
        get; private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleJoysticks(bool state)
    {
        gameObject.SetActive(state);
    }
}
