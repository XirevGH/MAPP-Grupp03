using UnityEngine;
using UnityEngine.Events;

public class YoyoController : PermanentProjectileWeapon
{
    public GameObject yoyo;
    public static int triggerNumber = 7;

    private void Start()
    {
        UnityAction action1 = new UnityAction(Attack);
        UnityAction action2 = new UnityAction(Blink);
        TriggerController.Instance.SetTrigger(triggerNumber, action1);
        TriggerController.Instance.SetTrigger(1, action2);
    }

    void Update()
    {
        if (amountOfProjectiles > transform.childCount)
        {
            AddYoyo();
            SetYoyoPosition();
        }
    }

    public override void Attack() 
    {
        if (gameObject.activeSelf)
        {
            int totalYoyo = transform.childCount;
            SoundManager.Instance.PlaySFX(attackSound, transform, 1);
            for (int i = 0; i < totalYoyo; i++)
            {
                transform.GetChild(i).GetComponent<Yoyo>().ActivateSuperMode();
            }
        }
    }

    public void Blink()
    {
        int totalYoyo = transform.childCount;

        for (int i = 0; i < totalYoyo; i++)
        {
            transform.GetChild(i).GetComponent<Yoyo>().Blink();
          
        }
     
    }

    private void SetYoyoPosition()
    {
        int totalYoyo = transform.childCount;
        float angle = 360f / totalYoyo;
        float nextAngle = 0;

        for (int i = 0; i < totalYoyo; i++)
        {
            transform.GetChild(i).GetComponent<Yoyo>().angle = nextAngle;
            transform.GetChild(i).GetComponent<Yoyo>().ResetSuperMode();
            nextAngle += angle;
        }
    }

    public void AddYoyo()
    {
        GameObject clone = Instantiate(yoyo, transform.position, Quaternion.identity, transform);
        clone.GetComponent<Yoyo>().SetDamage(damage);
    }
}
