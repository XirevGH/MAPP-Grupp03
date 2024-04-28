using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    public void OpenUpgradeWindow()
    {
        Time.timeScale = 0f;
        gameObject.gameObject.SetActive(true);
        TriggerController.instance.GetComponent<TriggerController>().ToggleTrigger();
        SoundManager.instance.GetComponent<SoundManager>().ToggleMusicPause();
    }

    public void CloseUpgradeWindow()
    {
        Time.timeScale = 1f;
        gameObject.gameObject.SetActive(false);
        TriggerController.instance.GetComponent<TriggerController>().ToggleTrigger();
        SoundManager.instance.GetComponent<SoundManager>().ToggleMusicPause();
    }
}
