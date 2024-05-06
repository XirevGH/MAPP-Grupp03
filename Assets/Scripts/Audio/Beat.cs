using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] private float increaseCooldownDuration, reduceCooldownDuration, MusicSpeedChange, timeToChange;
    [SerializeField] private GameObject circle, particle;
    private float percentageComplete, elapsedTime, beatLife;
    private Vector3 circleStartingScale;
    public bool isAlive;

    private void Start()
    {
        circleStartingScale = circle.transform.localScale;
        beatLife = (60f / (TriggerController.inGameCurrentTrackBPM / TriggerController.GetTrigger(BeatSpawner.triggerNumber).noteValue))
        / SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>().pitch;
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / beatLife;
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, percentageComplete);
            circle.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, percentageComplete);
            circle.transform.localScale = Vector3.Lerp(circleStartingScale, new Vector3(0, 0, 0), percentageComplete);
        }

        if (Mathf.FloorToInt(percentageComplete) == 1)
        {
            DestroyNoteWhenNotPickedUp();
        }
    }

    public void DestroyNoteWhenNotPickedUp()
    {
        Instantiate(particle, this.transform.position, Quaternion.identity);
        SoundManager.Instance.ChangePitch(false);
        Destroy(gameObject);
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Music Collider"))
        {
           SoundManager.Instance.ChangePitch(true);
            Destroy(gameObject);
        }
    }
}
