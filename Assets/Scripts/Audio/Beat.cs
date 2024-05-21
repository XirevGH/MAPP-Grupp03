using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] private float increaseCooldownDuration, reduceCooldownDuration, MusicSpeedChange, timeToChange;
    [SerializeField] private GameObject sprite, circle, particle;
    [SerializeField] private AudioClip collectedSound, notCollectedSound;
    private float percentageComplete, elapsedTime, beatLife;
    private Vector3 circleStartingScale;
    public bool isAlive;
    public int currencyToAdd;

    private void Start()
    {
        circleStartingScale = circle.transform.localScale;
        beatLife = (60f / (TriggerController.Instance.GetCurrentTrackBPM() / TriggerController.Instance.GetTrigger(BeatSpawner.triggerNumber).noteValue))
        / SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>().pitch;
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / beatLife;
            sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, percentageComplete);
            circle.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, percentageComplete);
            circle.transform.localScale = Vector3.Lerp(circleStartingScale, new Vector3(0, 0, 0), percentageComplete);
        }

        if (Mathf.FloorToInt(percentageComplete) == 1)
        {
            DestroyNoteWhenNotCollected();
        }
    }

    public void DestroyNoteWhenNotCollected()
    {
        Instantiate(particle, this.transform.position, Quaternion.identity);
        SoundManager.Instance.ChangePitch(false);
        SoundManager.Instance.PlaySFX(notCollectedSound, 1);
      
       
        Destroy(gameObject);
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Music Collider"))
        {  Player.Instance.AddCurrency(currencyToAdd);
            SoundManager.Instance.ChangePitch(true);
            SoundManager.Instance.PlaySFX(collectedSound, 1);
          
            Destroy(gameObject);
        }
    }
}
