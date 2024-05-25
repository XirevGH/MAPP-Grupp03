using UnityEngine;


public class PursePanel : MonoBehaviour
{
    [SerializeField] private GameObject tintPanel;

    public void OpenPurseWindow()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        TriggerController.Instance.ToggleTrigger(false);
        JoystickController.Instance.ToggleJoysticks(false);
        SoundManager.Instance.ToggleMusicPause(false);
        tintPanel.SetActive(true);
    }

    public void ClosePurseWindow()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        TriggerController.Instance.ToggleTrigger(true);
        JoystickController.Instance.ToggleJoysticks(true);
        SoundManager.Instance.ToggleMusicPause(true);
        tintPanel.SetActive(false);
    }
}
