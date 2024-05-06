using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    public void OpenUpgradeWindow()
    {
        Time.timeScale = 0f;
        gameObject.gameObject.SetActive(true);
        TriggerController.Instance.ToggleTrigger();
        SoundManager.Instance.ToggleMusicPause();
    }

    public void CloseUpgradeWindow()
    {
        Time.timeScale = 1f;
        gameObject.gameObject.SetActive(false);
        TriggerController.Instance.ToggleTrigger();
        SoundManager.Instance.ToggleMusicPause();
    }
}
