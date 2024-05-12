using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float itemPickupRadius;
    protected static GameObject playerObject;
    protected static Player player;
    
    private float pickupDistance = 0.3f;
    protected bool move;

    
    void Awake() {
        if(player == null){
            player =Player.Instance;
        }
        if(playerObject == null){
            playerObject = Player.Instance.gameObject;
        }

        move = false;
    }
    void FixedUpdate(){
        
        float distanceSquared = (player.transform.position - transform.position).sqrMagnitude;
        if (distanceSquared < itemPickupRadius * itemPickupRadius && !move){
            MoveToPlayer();
        }
        if (move && player != null)
        {   
            transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, speed * Time.deltaTime);
            if (distanceSquared < pickupDistance * pickupDistance){
               IndividualPickupAction();
               ReturnToPool();
            }
        } 
        
    }
    protected void MoveToPlayer()
    {   
        move = true;
    }

   protected virtual void ResetThis(){
        //For reseting the objekt or skript
   }

    public void ReturnToPool(){
        ResetThis();
        move = false;
        PoolController.Instance.ReturnPooledObject(gameObject.name, gameObject);
    }

    protected virtual void IndividualPickupAction(){
        
    }

}
