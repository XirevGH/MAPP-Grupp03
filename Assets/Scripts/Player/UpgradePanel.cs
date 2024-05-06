using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private GameObject tintPanel;

    public void OpenUpgradeWindow()
    {
        Time.timeScale = 0f;
        gameObject.gameObject.SetActive(true);
<<<<<<< Updated upstream
        TriggerController.Instance.ToggleTrigger();
        SoundManager.Instance.ToggleMusicPause();
=======
        tintPanel.SetActive(true);
        TriggerController.Instance.GetComponent<TriggerController>().ToggleTrigger();
        SoundManager.Instance.GetComponent<SoundManager>().ToggleMusicPause();
>>>>>>> Stashed changes
    }

    public void CloseUpgradeWindow()
    {
        Time.timeScale = 1f;
        gameObject.gameObject.SetActive(false);
<<<<<<< Updated upstream
        TriggerController.Instance.ToggleTrigger();
        SoundManager.Instance.ToggleMusicPause();
=======
        tintPanel.SetActive(false);
        TriggerController.Instance.GetComponent<TriggerController>().ToggleTrigger();
        SoundManager.Instance.GetComponent<SoundManager>().ToggleMusicPause();
>>>>>>> Stashed changes
    }
}
