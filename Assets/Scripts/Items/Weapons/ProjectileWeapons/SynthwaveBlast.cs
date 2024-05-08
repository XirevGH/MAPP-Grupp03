using UnityEngine;
using UnityEngine.Events;

public class SynthwaveBlast : ProjectileWeapon
{
    [SerializeField] GameObject synthwavePivotPrefab;


    private void Start()
    {
        UnityAction action = new UnityAction(Attack);
        TriggerController.Instance.SetTrigger(9, action);
    }

    public override void Attack()
    {
        if (gameObject.activeSelf) {

            SoundManager.Instance.PlaySFX(attackSound, transform, 1);
            for (int i = 0; i < amountOfProjectiles; i++) {
                float randomValue = Random.Range(0f, 360f);
                GameObject clone = Instantiate(synthwavePivotPrefab, transform);
                clone.transform.eulerAngles = new Vector3(0, 0, randomValue);
                clone.transform.GetChild(0).GetComponent<SynthwaveBolt>().SetDamage(damage);
                clone.transform.GetChild(0).GetComponent<SynthwaveBolt>().SetPenetration(penetration);
                

            }
        }
    }
}
