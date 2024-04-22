
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject parentForBeat, beat;

    public void SpawnBeat()
    {
       Instantiate(beat, transform.position, Quaternion.identity, parentForBeat.GetComponent<Transform>());
    }

    public void SetSpawnPosition()
    {
        transform.localPosition = new Vector2(Random.Range(-16.0f, 16.0f), Random.Range(-8.0f, 8.0f));
    }
}
