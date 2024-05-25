using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public GameObject leftJoystic;
    public GameObject rightJoystic;

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
        leftJoystic.SetActive(state);
        rightJoystic.SetActive(state);
    }
}
