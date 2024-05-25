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
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);

        if (isInitialized)
        {
            PlayClickSound();
        }
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);

        if (isInitialized)
        {
            PlayClickSound();
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
}
