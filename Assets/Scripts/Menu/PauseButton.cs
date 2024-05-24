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
            pauseButtonRectTransform.anchorMin = new Vector2(0.93f, pauseButtonRectTransform.anchorMin.y);
            pauseButtonRectTransform.anchorMax = new Vector2(0.99f, pauseButtonRectTransform.anchorMax.y);
            settingsManager.GetComponent<SettingsManager>().ChangePauseButtonSides(true);
        }
        else
        {
            pauseButtonRectTransform.anchorMin = new Vector2(0.01f, pauseButtonRectTransform.anchorMin.y);
            pauseButtonRectTransform.anchorMax = new Vector2(0.07f, pauseButtonRectTransform.anchorMax.y);
            settingsManager.GetComponent<SettingsManager>().ChangePauseButtonSides(false);
        }
    }
}
