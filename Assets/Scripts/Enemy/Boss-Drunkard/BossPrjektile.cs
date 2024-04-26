using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossPrjektile : MonoBehaviour{
    
    
    private float abiletySpeed;
    private float abiletyZise;

    private float abiletySlow;


    private float abiletyLifetime;

    private float spachLifetime;

    private Vector3 abiletyDirection;
    
    private float privuisDirection;
    private GameObject player;

    public GameObject SecondaryObjekt;

    

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

    private void Start() {
        privuisDirection = Vector3.Distance(player.transform.position, transform.position);
    }

     private void Update()
    {   
        transform.position += abiletyDirection * abiletySpeed * Time.deltaTime;
        abiletyLifetime -= Time.deltaTime;
        if (abiletyLifetime <= 0 || Vector3.Distance(player.transform.position, transform.position) <= 0 || privuisDirection < Vector3.Distance(player.transform.position, transform.position))
        {
            BotelSplaschZone();
        }
        privuisDirection = Vector3.Distance(player.transform.position, transform.position)* 1.01f;

    }

//ärinte färdig Och är använd av alla bossar.
    private void BotelSplaschZone(){
        if(SecondaryObjekt)
        {
            SecondaryObjekt = Instantiate(SecondaryObjekt, transform.position, Quaternion.identity);
            BossProjektilSplasch splasch = SecondaryObjekt.GetComponent<BossProjektilSplasch>();
            if (splasch != null)
            {
                splasch.Initialize( abiletyZise, abiletySlow, spachLifetime, player ); 
            }
        }
       

        Destroy(gameObject); 
    }
}