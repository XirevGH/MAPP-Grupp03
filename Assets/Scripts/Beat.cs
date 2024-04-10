using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Beat : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private GameObject endPoint;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        endPoint = GameObject.FindGameObjectWithTag("EndOfScreen");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rectTransform.position = Vector3.MoveTowards(rectTransform.position, endPoint.position, moveSpeed * Time.deltaTime);
        //rectTransform.Translate( Vector3.MoveTowards(rectTransform.position, endPoint.position, 1)  * moveSpeed * Time.deltaTime);
       // rectTransform.Translate(Vector3.up * Time.deltaTime, Space.World);

        rectTransform.position = Vector3.MoveTowards(this.rectTransform.position, endPoint.transform.position, moveSpeed);
      
    }
   

    private void Update()
    {
        if (this.rectTransform.position == endPoint.transform.position)
        {
            Debug.Log("Game Over?");

        }
    }

}
