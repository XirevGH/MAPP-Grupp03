using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private GameObject settingsManager;
    private RectTransform pauseButtonRectTransform;
     private void Awake()
    {
        pauseButtonRectTransform = GetComponent<RectTransform>(); 
        settingsManager = GameObject.FindGameObjectWithTag("SettingsManager");
        SetButtonPos(settingsManager.GetComponent<SettingsManager>().GetIsSetOnRight());

    }

    public void SetButtonPos(bool isRight)
    {
        if (isRight == true)
        {
            float offset = 0.1f;
            float newXAnchor = 1f - offset;
            pauseButtonRectTransform.anchorMin = new Vector2(newXAnchor, pauseButtonRectTransform.anchorMin.y);
            pauseButtonRectTransform.anchorMax = new Vector2(newXAnchor, pauseButtonRectTransform.anchorMax.y);
            settingsManager.GetComponent<SettingsManager>().ChangePauseButtonSides(true);
        }
        else
        {
            pauseButtonRectTransform.anchorMin = new Vector2(0f, pauseButtonRectTransform.anchorMin.y);
            pauseButtonRectTransform.anchorMax = new Vector2(0f, pauseButtonRectTransform.anchorMax.y);
            settingsManager.GetComponent<SettingsManager>().ChangePauseButtonSides(false);
        }
    }
}
