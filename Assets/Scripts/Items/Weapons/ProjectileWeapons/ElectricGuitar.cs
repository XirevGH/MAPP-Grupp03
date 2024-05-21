
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class ElectricGuitar : TetheringWeapon
{
    [SerializeField] private GameObject bolt;

    private void Start()
    {
        UnityAction action = new UnityAction(Attack);
        TriggerController.Instance.SetTrigger(6, action);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enemies.Remove(other.gameObject);
    }

    public override void Attack()
    {
        if (gameObject.activeSelf)
        {
            if(enemies.Count != 0)
            {
                SoundManager.Instance.PlaySFX(attackSound, 1);
                GameObject[] targetEnemies = GetClosestEnemies(AdjustTargetOverflow(amountOfTethers));
                for (int i = 0; i < targetEnemies.Length; i++)
                {
                    GameObject clone = Instantiate(bolt);
                    clone.GetComponent<ElectricBolt>().SetPlayerObject(gameObject);
                    clone.GetComponent<ElectricBolt>().SetTargetUnit(targetEnemies[i]);
                    clone.SetActive(true);
                }

            }
        } 
    }

    public float GetDamage()
    {
        return damage;
    }
}
