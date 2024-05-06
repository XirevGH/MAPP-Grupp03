using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Boss_AOE_Slow : MonoBehaviour
{
    private float abiletyZise;
    private float abiletyLifetime;
    private float abiletySlow;
    private GameObject player;

    
    public void Initialize(float zise, float slow, float time, GameObject player)
    {
        this.abiletyZise = zise;  
        this.abiletySlow = slow /100;  
        this.abiletyLifetime = time;
        this.player = player;
    }


    public void Start(){

        transform.localScale = new Vector3(abiletyZise, abiletyZise, abiletyZise); 
    }
    private void FixedUpdate(){
        abiletyLifetime -= Time.deltaTime;
        if(abiletyLifetime <= 0){
            if(player != null){
                Player.Instance.GetComponent<PlayerMovement>().DecreaseMovementSpeed(1);
             //   player.GetComponent<PlayerMovement>().DecreaseMovementSpeed(1);
                Destroy(gameObject); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  
        {  other.GetComponent<PlayerMovement>().DecreaseMovementSpeed(abiletySlow);
        }
    }
}
