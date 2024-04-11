using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform pos;

    private float horizontalValue;
    private float verticalValue;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");
        if(horizontalValue < 0f)
        {
            FlipSprite(true);
        }
        if (horizontalValue > 0f)
        {
            FlipSprite(false);
        }
    }

    private void FlipSprite(bool flip)
    {
        rend.flipX = flip;
    }

    private void FixedUpdate()
    {
        pos.position = new Vector2(pos.position.x + horizontalValue * moveSpeed * Time.deltaTime, pos.position.y + verticalValue * moveSpeed * Time.deltaTime);
    }
}










