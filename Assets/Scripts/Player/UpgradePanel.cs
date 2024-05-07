using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private GameObject tintPanel;

    public void OpenUpgradeWindow()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        TriggerController.Instance.ToggleTrigger();
        SoundManager.Instance.ToggleMusicPause();
        tintPanel.SetActive(true);
       
    }

    public void CloseUpgradeWindow()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        TriggerController.Instance.ToggleTrigger();
        SoundManager.Instance.ToggleMusicPause();
        tintPanel.SetActive(false);
      
    }
}
