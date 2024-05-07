
using UnityEngine;
using UnityEngine.Events;

public class BeatSpawner : MonoBehaviour
{
    public static int triggerNumber = 7;
    [SerializeField] private GameObject parentForBeat, beat;


    private void Start()
    {
        UnityAction action1 = new UnityAction(SetSpawnPosition);
        UnityAction action2 = new UnityAction(SpawnBeat);
        TriggerController.Instance.SetTrigger(triggerNumber, action1);
        TriggerController.Instance.SetTrigger(triggerNumber, action2);
    }

    public void SpawnBeat()
    {
        if (parentForBeat == null)
        {
            parentForBeat = GameObject.FindGameObjectWithTag("BeatContainer");
        }
       Instantiate(beat, transform.position, Quaternion.identity, parentForBeat.GetComponent<Transform>());
    }

    public void SetSpawnPosition()
    {
        transform.localPosition = new Vector2(Random.Range(-16.0f, 16.0f), Random.Range(-8.0f, 8.0f));
    }

}
