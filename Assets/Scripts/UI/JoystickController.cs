using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour
{
    public DynamicJoystick leftJoystic;
    public DynamicJoystick rightJoystic;

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
        leftJoystic.gameObject.SetActive(state);
        rightJoystic.gameObject.SetActive(state);
        leftJoystic.handle.anchoredPosition = Vector2.zero;
        rightJoystic.handle.anchoredPosition = Vector2.zero;
        leftJoystic.input = Vector2.zero;
        rightJoystic.input = Vector2.zero;
       
    }


}
