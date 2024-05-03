using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] private float increaseCooldownDuration, reduceCooldownDuration, MusicSpeedChange, timeToChange;
    [SerializeField] private GameObject circle, particle;
    private float percentageComplete, elapsedTime, beatLife;
    private Vector3 circleStartingScale;
    private bool isAlive;

    private void Start()
    {
        isAlive = true;
        circleStartingScale = circle.transform.localScale;
        beatLife = (60f / (TriggerController.Instance.GetComponent<TriggerController>().GetCurrentTrackBPM() / TriggerController.Instance.GetComponent<TriggerController>().GetTrigger(0).noteValue))
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
        SoundManager.Instance.ChangePitch1(false);
        Destroy(gameObject);
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Music Collider"))
        {
           SoundManager.Instance.ChangePitch1(true);
            Destroy(gameObject);
        }
    }
}
