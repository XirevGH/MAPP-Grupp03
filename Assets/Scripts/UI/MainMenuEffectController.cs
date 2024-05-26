using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuEffectController : MonoBehaviour
{
    [SerializeField] private Image[] buttonImages;
    [SerializeField] private Sprite playLit, playUnlit, upgradesLit, upgradesUnlit, settingsLit, settingsUnlit;
    [SerializeField] private AudioClip off1, off2, on1, on2, on3, on4;
    [SerializeField] private float minWaitTime, maxWaitTime, minFlickerTime, maxFlickerTime;

    private AudioSource buzzPlayer, src;

    void Start()
    {
        StopAllCoroutines();
        StartCoroutine(FlickerLights());
        buzzPlayer = GetComponent<AudioSource>();
        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.volume = 0.5f;
        src.panStereo = -0.5f;
    }

    private void Awake()
    {
        StopAllCoroutines();
        StartCoroutine(FlickerLights());
    }

    public void ResetCoroutines()
    {
        StopAllCoroutines();
        StartCoroutine(FlickerLights());
    }

    private IEnumerator FlickerLights()
    {
        float randTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(randTime);
        int randButton = Random.Range(0, buttonImages.Length);
        if(randButton == 0)
        {
            buttonImages[0].sprite = playUnlit;
            src.PlayOneShot(off1);
            buzzPlayer.volume = 0.1f;
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            buttonImages[0].sprite = playLit;
            src.PlayOneShot(on1);
            buzzPlayer.volume = 0.3f;
        }
        if (randButton == 1)
        {
            buttonImages[1].sprite = upgradesUnlit;
            src.PlayOneShot(off1);
            buzzPlayer.volume = 0.1f;
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            buttonImages[1].sprite = upgradesLit;
            src.PlayOneShot(on1);
            buzzPlayer.volume = 0.3f;
        }
        if (randButton == 2)
        {
            buttonImages[2].sprite = settingsUnlit;
            src.PlayOneShot(off1);
            buzzPlayer.volume = 0.1f;
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            buttonImages[2].sprite = settingsLit;
            src.PlayOneShot(on1);
            buzzPlayer.volume = 0.3f;
        }
        StartCoroutine(FlickerLights());
    }
}
