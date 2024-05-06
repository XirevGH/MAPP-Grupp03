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
   

    // [SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioClip clickSound;

    private float baseClickVolume;
    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
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

       // audioSource.volume = baseClickVolume * volume;
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
        pauseButtonRectTransform.anchorMin = new Vector2(0f, pauseButtonRectTransform.anchorMin.y);
        pauseButtonRectTransform.anchorMax = new Vector2(0f, pauseButtonRectTransform.anchorMax.y);
      // audioSource.PlayOneShot(clickSound);
    }

    public void OnRightButtonClick()
    {
        float offset = 0.1f;
        float newXAnchor = 1f - offset;
        pauseButtonRectTransform.anchorMin = new Vector2(newXAnchor, pauseButtonRectTransform.anchorMin.y);
        pauseButtonRectTransform.anchorMax = new Vector2(newXAnchor, pauseButtonRectTransform.anchorMax.y);
     //  audioSource.PlayOneShot(clickSound);
    }
    public void TurnOnHealthBar()
    {
        healthBar.SetActive(true);
    }

    public void TurnOffHealthBar()
    {
        healthBar.SetActive(false);
    }
}
