using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private Player player;

    private void FixedUpdate()
    {
        /*if (player.GetCurrentItems().Contains(this))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }*/
    }
}
