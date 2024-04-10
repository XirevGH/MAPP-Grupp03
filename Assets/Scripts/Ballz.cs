using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Ballz : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private RectTransform endPoint;
    [SerializeField] private RectTransform spawnPoint;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rectTransform.position = Vector3.MoveTowards(rectTransform.position, endPoint.position, moveSpeed * Time.deltaTime);
        //rectTransform.Translate( Vector3.MoveTowards(rectTransform.position, endPoint.position, 1)  * moveSpeed * Time.deltaTime);
       // rectTransform.Translate(Vector3.up * Time.deltaTime, Space.World);

        rectTransform.position = Vector3.MoveTowards(this.rectTransform.position, endPoint.position, moveSpeed);
        //Debug.Log();
    }
   

    private void Update()
    {
        if (this.rectTransform.position == endPoint.position)
        {
            Debug.Log("Game Over?");

        }
    }

}
