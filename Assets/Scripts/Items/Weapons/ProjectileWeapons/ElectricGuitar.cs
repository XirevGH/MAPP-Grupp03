
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElectricGuitar : TetheringWeapon
{
    [SerializeField] private GameObject bolt;

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
        GameObject[] targetEnemies = GetClosestEnemies(AdjustTargetOverflow(amountOfTethers));
        for(int i = 0; i < targetEnemies.Length; i++)
        {
            GameObject clone = Instantiate(bolt);
            clone.GetComponent<ElectricBolt>().SetPlayerObject(gameObject);
            clone.GetComponent<ElectricBolt>().SetTargetUnit(targetEnemies[i]);
            clone.SetActive(true);
        }
        //StartCooldown();
    }

    public float GetDamage()
    {
        return damage;
    }
}
