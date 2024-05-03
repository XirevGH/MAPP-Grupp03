using UnityEngine;

public class YoyoController : PermanentProjectileWeapon
{
    public GameObject yoyo;

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
        int totalYoyo = transform.childCount;
        for (int i = 0; i < totalYoyo; i++)
        {
             transform.GetChild(i).GetComponent<Yoyo>().ActivateSuperMode();
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
