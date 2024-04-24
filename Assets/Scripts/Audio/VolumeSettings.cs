using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    public PauseMenu pauseMenu;
    public RectTransform pauseButtonRectTransform;
    private void Start()
    {
   if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        } else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        SetSFXVolume();
    }

    public void OnLeftButtonClick()
    {
        pauseButtonRectTransform.localPosition = new Vector3(0f, -31.45996f, 0f);
    }

    public void OnRightButtonClick()
    {
        pauseButtonRectTransform.localPosition = new Vector3(1730f, -31.45996f, 0f);
    }
}
