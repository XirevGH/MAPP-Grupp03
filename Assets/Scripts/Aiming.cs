using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Aiming : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    public Transform weapons;


    private void FixedUpdate()
    {
        Debug.Log(dynamicJoystick.Horizontal + ", " + dynamicJoystick.Vertical);
    }
}
