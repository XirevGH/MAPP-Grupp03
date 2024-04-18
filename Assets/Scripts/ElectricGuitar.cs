
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElectricGuitar : ProjectileWeapon
{
    [SerializeField] private GameObject bolt;
    

    private HashSet<GameObject> enemies = new HashSet<GameObject>();
    private GameObject closestEnemy;

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
        GameObject[] targetEnemies = GetClosestEnemies(AdjustTargetOverflow(amountOfTargets));
        for(int i = 0; i < targetEnemies.Length; i++)
        {
            GameObject clone = Instantiate(bolt);
            clone.GetComponent<ElectricBolt>().SetTargetUnit(targetEnemies[i]);
            clone.SetActive(true);
        }
        StartCooldown();
    }

    private int AdjustTargetOverflow(int amountOfTargets)
    {
        if(enemies.Count < amountOfTargets)
        {
            return enemies.Count;
        }
        else
        {
            return amountOfTargets;
        }
    }

    public GameObject[] GetClosestEnemies(int amountOfTargets)
    {
        SortedSet<GameObject> sortedEnemies = new SortedSet<GameObject>(new GameObjectComparer());
        foreach(GameObject enemy in enemies)
        {
            sortedEnemies.Add(enemy);
        }
        return sortedEnemies.Take(amountOfTargets).ToArray();
    }

    public float GetDamage()
    {
        return damage;
    }

    public void UpgradeTargetAmount(int amount)
    {
        amountOfTargets += amount;
    }
}
