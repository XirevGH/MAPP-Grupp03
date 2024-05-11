using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

        attackDelayTime = ((60f / BPM) / 2 ) / pitch;
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
            this.GetComponent<AudioSource>().Play();
            SoundManager.Instance.PlaySFX(attackSound, 1 - (i * 0.1f));

            yield return new WaitForSeconds(attackDelayTime);

        }

    }
}
