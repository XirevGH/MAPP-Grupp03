using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform pos;

    private float horizontalValue;
    private float verticalValue;
    public bool canMove;
 


    void Start()
    {
        
        canMove = true;
        
        
    }

   

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }


        if (!canMove) return;

        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");
  
    }



    private void FixedUpdate()
    {
        if (!canMove)
            return;

        pos.position = new Vector2(pos.position.x + horizontalValue * moveSpeed * Time.deltaTime, pos.position.y + verticalValue * moveSpeed * Time.deltaTime);

    }






    


}










