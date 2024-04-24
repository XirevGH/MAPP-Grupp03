using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPrjektile : MonoBehaviour{
    
     
    private float abiletySpeed;
    private float abiletyZise;

    private float abiletySlow;


    private float abiletyLifetime;

    private float spachLifetime;

    private Vector3 abiletyDirection;
    
    private GameObject player;

    public GameObject spachArea;

    

    public void Initialize(float speed, float zise, float slow, float time, Vector3 direction ,GameObject player)
    {
        
        this.abiletySpeed = speed;
        this.abiletyZise = zise;  
        this.abiletySlow = slow;  
        this.abiletyDirection = direction;
        this.abiletyLifetime = time;
        this.spachLifetime = time;
        this.player = player;
        
       
        
    }

     private void Update()
    {
        transform.position += abiletyDirection * abiletySpeed * Time.deltaTime;
        abiletyLifetime -= Time.deltaTime;
        if (abiletyLifetime <= 0 || Vector3.Distance(player.transform.position, transform.position) < 4)
        {
            BotelSplaschZone();
        }
    }

    private void BotelSplaschZone(){
        if(spachArea)
        {
            spachArea = Instantiate(spachArea, transform.position, Quaternion.identity);
            BossProjektilSplasch splasch = spachArea.GetComponent<BossProjektilSplasch>();
            if (splasch != null)
            {
                splasch.Initialize( abiletyZise, abiletySlow, spachLifetime, player ); 
            }
        }
       

        Destroy(gameObject); 
    }
}