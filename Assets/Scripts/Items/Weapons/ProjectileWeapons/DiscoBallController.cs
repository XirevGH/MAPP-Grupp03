using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscoBallController : ProjectileWeapon
{    
    [SerializeField] private GameObject discoBall;
    [SerializeField] private float blinkTime;
    public List<GameObject> activeDiscoBalls = new();


    private float BPM, attackDelayTime, pitch;
    private AudioSource source;

    public static DiscoBallController Instance
    {
        get; private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    private void Start()
    {
        UnityAction action1 = new UnityAction(Attack);
        UnityAction action2 = new UnityAction(Blink);
        TriggerController.Instance.SetTrigger(4, action1);
        TriggerController.Instance.SetTrigger(1, action2);
    }

    private void FixedUpdate()
    {
        BPM = TriggerController.Instance.GetCurrentTrackBPM();
        source = SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>();
        pitch = source.pitch;

        attackDelayTime = ((60f / BPM) / 2) / pitch;
    }


    public void Blink()
    {

        if (activeDiscoBalls.Count == 0)
        {
            return;
        }
        else
        {

            for (int i =0; i < activeDiscoBalls.Count; i++)
            {
                GameObject discoBall = activeDiscoBalls[i];

                if (discoBall == null)
                {
                    activeDiscoBalls.RemoveAt(i);
                }
                else
                {
                    discoBall.GetComponent<DiscoBall>().Blink();
                }
            }
        }
        
    }

    public override void Attack()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine("AttackDelay");
        }

    }

    private IEnumerator AttackDelay()
    {
        for (int i = 0; i < amountOfProjectiles; i++)
        {
            GameObject clone = Instantiate(discoBall, transform.position, Quaternion.identity);
            activeDiscoBalls.Add(clone);
            clone.GetComponent<DiscoBall>().SetDamage(damage);
            clone.GetComponent<DiscoBall>().SetPenetration(penetration);
            SoundManager.Instance.PlaySFX(attackSound, 1 - (i * 0.1f));
        
            yield return new WaitForSeconds(attackDelayTime);
    
        }

    }
}