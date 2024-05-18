
using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BeatSpawner : MonoBehaviour
{
    public static int triggerNumber = 7;
    [SerializeField] private GameObject beat;


    private void Start()
    {
        UnityAction action1 = new UnityAction(SetSpawnPosition);
        UnityAction action2 = new UnityAction(SpawnBeat);
        TriggerController.Instance.SetTrigger(triggerNumber, action1);
        TriggerController.Instance.SetTrigger(triggerNumber, action2);
    }

    public void SpawnBeat()
    {
       
       Instantiate(beat, transform.position, Quaternion.identity);
    }

    public void SetSpawnPosition()
    {
        transform.localPosition = RandomizeSpawnPosition();
    }

    private Vector2 RandomizeSpawnPosition()
    {
        float x = Random.Range(4f, 16.0f);
        float y = Random.Range(4, 8.0f);
        static float randomSign(float sign) => Random.Range(0, 2) == 0 ? sign : -sign;
        x = randomSign(x);
        y = randomSign(y);
        Vector2 spawnPosition = new(x, y);

        return spawnPosition;
    }

}
