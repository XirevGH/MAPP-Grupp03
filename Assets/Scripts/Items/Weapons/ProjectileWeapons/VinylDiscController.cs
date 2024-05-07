using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class VinylDiscController : ProjectileWeapon
{
    [SerializeField] private GameObject vinylDisc;
    [SerializeField] private float attackDelayTime;
    public static int triggerNumber = 7;

    private Vector3 playerPosition;
    private float BPM;

    private AudioSource source;
    private float pitch;

    private void Start()
    {
        UnityAction action = new UnityAction(Attack);
        TriggerController.Instance.SetTrigger(triggerNumber, action);
    }

    private void FixedUpdate()
    {
        BPM = TriggerController.Instance.GetCurrentTrackBPM();
        source = SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>();
        pitch = source.pitch;

        attackDelayTime = (60f / (BPM  / pitch)) / 2;
    }

    public override void Attack()
    {
        if (gameObject.activeSelf) { 
            StartCoroutine("AttackDelay");
        }
    }

    private IEnumerator AttackDelay()
    {
        for (int i = 0; i < amountOfProjectiles; i++)
        {
            playerPosition = player.transform.position;
            GameObject clone = Instantiate(vinylDisc, playerPosition, Quaternion.identity);
            clone.GetComponent<Projectile>().SetDamage(damage);
            clone.GetComponent<Projectile>().SetPenetration(penetration);
            clone.GetComponent<VinylDisc>().isAtPlayer = true;

            yield return new WaitForSeconds(attackDelayTime);

            playerPosition = player.transform.position;
            GameObject clone1 = Instantiate(vinylDisc, playerPosition, Quaternion.identity);
            clone1.GetComponent<Projectile>().SetDamage(damage);
            clone1.GetComponent<Projectile>().SetPenetration(penetration);
            clone1.GetComponent<VinylDisc>().isAtPlayer = true;
        }

    }

    public override int GetIncreaseProjectileCountCost()
    {
        return projectileUpgradeCost;
    }

    public override int GetIncreasePenetrationAmountCost()
    {
        return penetrationUpgradeCost;
    }

    public override int GetIncreaseDamageCost()
    {
        return damageUpgradeCost;
    }
}
