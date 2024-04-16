using UnityEngine;

public class Aiming : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;

    private void FixedUpdate()
    {
        if (dynamicJoystick.Horizontal == 0 && dynamicJoystick.Vertical == 0)
        {
            return;
        }
        if (dynamicJoystick.Horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0f, -180f, dynamicJoystick.Vertical * 90);
        }
        if (dynamicJoystick.Horizontal > 0) 
        {
            transform.eulerAngles = new Vector3(0f, 0f, dynamicJoystick.Vertical * 90);
        }

    }
}
