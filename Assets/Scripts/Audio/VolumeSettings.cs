using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] public GameObject healthBar;

    public PauseMenu pauseMenu;
    public RectTransform pauseButtonRectTransform;
   

  

    private float baseClickVolume;
    private bool isInitialized = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }

        isInitialized = true;
    }

    public void SetMusicVolume()
    {
        float volume;
        if (musicSlider.value < 1)
        {
            volume = 0.001f;
        }
        else
        {
            volume = musicSlider.value;
        }
        
        myMixer.SetFloat("music", Mathf.Log10(volume / 10) * 30);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume;
        if (SFXSlider.value < 1)
        {
            volume = 0.001f;
        }
        else
        {
            volume = SFXSlider.value;
        }
        myMixer.SetFloat("SFX", Mathf.Log10(volume / 10) * 30);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        if (isInitialized)
        {
            PlaySliderSound();
        }
    }


    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        SetSFXVolume();
    }

   
    public void TurnOnHealthBar()
    {
        healthBar.SetActive(true);
       // SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void TurnOffHealthBar()
    {
        healthBar.SetActive(false);
        //SoundManager.Instance.GetComponent<SoundManager>().Click();
    }

    public void PlayClickSound()
    {
        SoundManager.Instance.Click();
    }

    public void PlaySliderSound()
    {
        SoundManager.Instance.SliderSound();
    }
}
