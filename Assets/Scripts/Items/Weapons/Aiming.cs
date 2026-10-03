using UnityEngine;
using UnityEngine.SceneManagement;

public class Aiming : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    public Transform arrow;
    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (dynamicJoystick == null)
            {
                DynamicJoystick[] dynamicJoysticks = FindObjectsOfType<DynamicJoystick>();
                foreach (DynamicJoystick joystick in dynamicJoysticks)
                {
                    if (joystick.GetPosition() == "Right")
                    {
                        dynamicJoystick = joystick;
                    }
                }
            }
            if (dynamicJoystick.Horizontal == 0 && dynamicJoystick.Vertical == 0)
            {
                return;
            }

            // Convert joystick input vector into rotation angle in degrees
            Vector2 point = new Vector2(dynamicJoystick.Horizontal, dynamicJoystick.Vertical);
            float angleInRadians = Mathf.Atan2(point.y, point.x);
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
            arrow.eulerAngles = new Vector3(0f, 0f, angleInDegrees);

            // Flip sprite across X-axis when aiming left to maintain proper facing orientation
            if (point.x < 0)
            {
                transform.eulerAngles = new Vector3(180f, 360f, -angleInDegrees);
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, angleInDegrees);
            }
        }
    }
}
