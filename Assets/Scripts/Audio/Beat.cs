using Unity.VisualScripting;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] private float moveSpeed, increaseCooldownDuration, reduceCooldownDuration, MusicSpeedChange;
    [SerializeField] private GameObject circle, particle;
    public static float totalChangedInBPM = 0;

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
            SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().pitch = SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().pitch - MusicSpeedChange;
        }
        
        totalChangedInBPM = totalChangedInBPM - MusicSpeedChange;
        Instantiate(particle, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Music Collider"))
        {

            SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().pitch = SoundManager.instance.transform.GetChild(0).GetComponent<AudioSource>().pitch + MusicSpeedChange;
            totalChangedInBPM = totalChangedInBPM + MusicSpeedChange;
            Destroy(gameObject);
        }
    }
}
