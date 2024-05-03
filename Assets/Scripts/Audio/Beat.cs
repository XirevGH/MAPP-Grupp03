using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] private float increaseCooldownDuration, reduceCooldownDuration, MusicSpeedChange, timeToIncrease;
    [SerializeField] private GameObject circle, particle;
    private float percentageComplete, elapsedTime, beatLife;
    private Vector3 circleStartingScale;

    private void Start()
    {
        circleStartingScale = circle.transform.localScale;
        beatLife = (60f / (TriggerController.instance.GetComponent<TriggerController>().GetCurrentTrackBPM() / TriggerController.instance.GetComponent<TriggerController>().GetTrigger(0).noteValue))
        / SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().pitch;
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

        if (SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().pitch <= 0.5)
        {
            SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().pitch = 0.5f;
        }
        else
        {
            StartCoroutine(ChangePitch(false));
        }
        
       
        Instantiate(particle, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator ChangePitch(bool increasePitch)
    {
        int direction = increasePitch ? 1 : -1;
        float timeElapsed = 0;
        AudioSource audioSource = SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>();
        
        while (timeElapsed < timeToIncrease)
        {
            audioSource.pitch = Mathf.Lerp(1, 1.5f , timeElapsed / timeToIncrease);
            Debug.Log(timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;

        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Music Collider"))
        {
            StartCoroutine(ChangePitch(true));

            Destroy(gameObject);
        }
    }
}
