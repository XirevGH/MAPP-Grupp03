using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuEffectController : MonoBehaviour
{
    [SerializeField] private Image[] buttonImages;
    [SerializeField] private Sprite playLit, playUnlit, upgradesLit, upgradesUnlit, settingsLit, settingsUnlit;
    [SerializeField] private float minWaitTime, maxWaitTime, minFlickerTime, maxFlickerTime;

    void Start()
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
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            buttonImages[0].sprite = playLit;
        }
        if (randButton == 1)
        {
            buttonImages[1].sprite = upgradesUnlit;
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            buttonImages[1].sprite = upgradesLit;
        }
        if (randButton == 2)
        {
            buttonImages[2].sprite = settingsUnlit;
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            buttonImages[2].sprite = settingsLit;
        }
        StartCoroutine(FlickerLights());
    }
}
