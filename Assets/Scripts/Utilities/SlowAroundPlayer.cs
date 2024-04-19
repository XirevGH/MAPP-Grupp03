using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SlowAroundPlayer : MonoBehaviour
{
    [SerializeField] private float slowSpeedPercent;
    private HashSet<GameObject> enemies = new HashSet<GameObject>();
    private Dictionary<GameObject, float> enemiesOriginalMoveSpeed = new Dictionary<GameObject, float>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().thisMovementSpeed = other.gameObject.GetComponent<Enemy>().thisMovementSpeed * slowSpeedPercent;
            other.gameObject.GetComponent<Enemy>().isSlow = true;
            enemies.Add(other.gameObject);
            enemiesOriginalMoveSpeed.Add(other.gameObject, other.gameObject.GetComponent<Enemy>().thisMovementSpeed);
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().isSlow = false;
            other.gameObject.GetComponent<Enemy>().thisMovementSpeed = Enemy.movementSpeed;
            enemies.Remove(other.gameObject);
            enemiesOriginalMoveSpeed.Remove(other.gameObject);

        }
    }
}
