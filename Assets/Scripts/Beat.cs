
using Unity.VisualScripting;
using UnityEngine;



public class Beat : MonoBehaviour
{
    [SerializeField] private float moveSpeed, increaseCooldownDuration, reduceCooldownDuration;
    [SerializeField] private GameObject circle, particle;
    //private GameObject currentWeapon;
    [SerializeField] GameObject[] weapons;
    //private RectTransform rectTransform;
    private float time;
    private void Start()
    {
        weapons = GameObject.FindGameObjectsWithTag("Weapon");

    }

    void FixedUpdate()
    {

        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, time);
        circle.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, time);

        circle.transform.localScale = Vector3.Lerp(new Vector3(10, 10, 10), new Vector3(1, 1, 1), time);
        time += Time.deltaTime * 0.4f;
        if (Mathf.FloorToInt(time) == 1)
        {
            DestroyNote();
        }

    }

    public void DestroyNote()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.GetComponent<Weapon>().ChangeCooldownDuration(+increaseCooldownDuration);
        }

        Instantiate(particle, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Music Collider"))
        {
            foreach (GameObject weapon in weapons)
            {
                weapon.GetComponent<Weapon>().ChangeCooldownDuration(-reduceCooldownDuration);
            }

            Destroy(gameObject);
        }
    }



}
