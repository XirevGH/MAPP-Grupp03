using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static BeatSpawnerController;

public class Beat : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject circle, particle;
    private GameObject currentWeapon;
    //private RectTransform rectTransform;
    private float time;
    private void Start()
    {
        //rectTransform = GetComponent<RectTransform>();
        //endPoint = GameObject.FindGameObjectWithTag("EndOfScreen");
        currentWeapon = GameObject.FindGameObjectWithTag("Weapon");
      

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //rectTransform.position = Vector3.MoveTowards(this.rectTransform.position, endPoint.transform.position, moveSpeed);

        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, time);
        circle.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, time);

        circle.transform.localScale = Vector3.Lerp(new Vector3(10, 10, 10), new Vector3(1, 1, 1), time);
        time += Time.deltaTime * 0.5f;

        //if (GetComponent<SpriteRenderer>().color == Color.red)
        //{
        //    currentWeapon.GetComponent<Weapon>().ChangeCooldownDuration(+0.1f);

        //    Debug.Log("reduce attack speed");
        //    Debug.Log(currentWeapon.GetComponent<Weapon>().GetCooldownDuration());
        //    Destroy(this);
        //}

    }

    public void DestroyNote()
    {
        currentWeapon.GetComponent<Weapon>().ChangeCooldownDuration(+0.05f);

        Debug.Log("reduce attack speed");
        Debug.Log(currentWeapon.GetComponent<Weapon>().GetCooldownDuration());
        Instantiate(particle, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

 


    private void Update()
    {
        //if (circle.transform.localScale == new Vector3(1, 1, 1))
        //{
        //    DestroyNote();
        //}
    }

}
