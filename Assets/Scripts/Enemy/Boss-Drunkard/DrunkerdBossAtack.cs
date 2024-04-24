using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkerdBossAtack : MonoBehaviour
{
    public float abiletySpeed;
    public float abiletyZise;
    public float abiletySlow;
    public float abiletyLifetime;
    public Transform bossPosition;
    [SerializeField]private GameObject bossAtckObject;

    public GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ShootNoteAttPalyer()
    {
        if(bossAtckObject)
        {
            GameObject clone = Instantiate(bossAtckObject, bossPosition.position, Quaternion.identity);
            BossPrjektile bossAtckProjektile = clone.GetComponent<BossPrjektile>();
            if (bossAtckProjektile != null)
            {
                Vector3 direction = (player.transform.position - bossPosition.position).normalized;
                bossAtckProjektile.Initialize( abiletySpeed,  abiletyZise,  abiletySlow, abiletyLifetime, direction ,player); 
            }
        }
    }

}
