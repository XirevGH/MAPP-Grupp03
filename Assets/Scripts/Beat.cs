using Unity.VisualScripting;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] private float moveSpeed, increaseCooldownDuration, reduceCooldownDuration, MusicSpeedChange;
    [SerializeField] private GameObject circle, particle, triggerController, soundManager;
    [SerializeField] private GameObject[] weapons;
    public static float totalChangedInBPM = 0;

    private float percentageComplete, elapsedTime, beatLife;
    private Vector3 circleStartingScale;

    private void Start()
    {
        triggerController = GameObject.FindGameObjectWithTag("TriggerController");
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        circleStartingScale = circle.transform.localScale;
        beatLife = (60f / (triggerController.GetComponent<TriggerController>().GetCurrentTrackBPM() / triggerController.GetComponent<TriggerController>().GetTrigger(0).noteValue)) / soundManager.transform.GetChild(0).GetComponent<AudioSource>().pitch;
    }
    private void Update()
    {
     
        
    }

    void FixedUpdate()
    {
       
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
            if(weapon.GetComponent<Weapon>() != null)
            {
                //weapon.GetComponent<Weapon>().ChangeCooldownDuration(+increaseCooldownDuration);
            }
        }
        if (soundManager.transform.GetChild(0).GetComponent<AudioSource>().pitch <= 0.5)
        {
            soundManager.transform.GetChild(0).GetComponent<AudioSource>().pitch = 0.5f;
        }
        else
        {
            soundManager.transform.GetChild(0).GetComponent<AudioSource>().pitch = soundManager.transform.GetChild(0).GetComponent<AudioSource>().pitch - MusicSpeedChange;
        }
        
        totalChangedInBPM = totalChangedInBPM - MusicSpeedChange;
        Instantiate(particle, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Music Collider"))
        {
            foreach (GameObject weapon in weapons)
            {
                if(weapon.GetComponent<Weapon>() != null)
                {
                    //weapon.GetComponent<Weapon>().ChangeCooldownDuration(-reduceCooldownDuration);
                }
            }

            soundManager.transform.GetChild(0).GetComponent<AudioSource>().pitch = soundManager.transform.GetChild(0).GetComponent<AudioSource>().pitch + MusicSpeedChange;
            totalChangedInBPM = totalChangedInBPM + MusicSpeedChange;
            Destroy(gameObject);
        }
    }
}
