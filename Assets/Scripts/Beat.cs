
using Unity.VisualScripting;
using UnityEngine;



public class Beat : MonoBehaviour
{
    [SerializeField] private float moveSpeed, increaseCooldownDuration, reduceCooldownDuration;
    [SerializeField] private GameObject circle, particle, gameController, TriggerController;
    [SerializeField] private GameObject[] weapons;
    private float percentageComplete, elapsedTime, beatLife;


    private Vector3 circleStartingScale;
    private void Start()
    {
        TriggerController = GameObject.FindGameObjectWithTag("TriggerController");
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        circleStartingScale = circle.transform.localScale;
       

    }

    void FixedUpdate()
    {
        beatLife = 60f / (gameController.GetComponent<GameController>().GetCurrentTrackBPM() / TriggerController.GetComponent<TriggerController>().GetTrigger(0).noteValue);
        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / beatLife;
        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, percentageComplete);
        circle.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, percentageComplete);

        circle.transform.localScale = Vector3.Lerp(circleStartingScale, new Vector3(0, 0, 0), percentageComplete);

        
      
        if (Mathf.FloorToInt(percentageComplete) == 1)
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
