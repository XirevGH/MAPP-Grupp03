using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource, musicSource2, ambeintSource;
    [SerializeField] private AudioClip[] musicTracks;
    [SerializeField] public Slider slider;
    //[SerializeField] public Slider slidertoFind;
    public static SoundManager Instance;
    public static int sliderValue;
    public int currentScene;
    private bool isOnePlaying;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else

        {
            Destroy(gameObject);
        }
        slider = GameObject.FindGameObjectWithTag("volumeSlider").GetComponent<Slider>();
    }


    void Start()
    {
        isOnePlaying = true;
        musicSource = GetComponent<AudioSource>();
        musicSource2 = GetComponent<AudioSource>();

        //AudioListener.volume = PlayerPrefs.GetFloat("volume1");
        //slider.value = PlayerPrefs.GetFloat("volume");
        //SoundMannerger.Instance.ChangeMasterVolume(slider.value);
        //slider.onValueChanged.AddListener(val => SoundMannerger.Instance.ChangeMasterVolume(val));
        AudioListener.volume = PlayerPrefs.GetFloat("volume1");
        slider.value = PlayerPrefs.GetFloat("volume");
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene >= 4)
        {
            musicSource.volume = 0;
            ambeintSource.volume = 0;
            

        }

        slider = GameObject.FindGameObjectWithTag("volumeSlider").GetComponent<Slider>();

        slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
        SoundManager.Instance.ChangeMasterVolume(slider.value);

       

    }

    private void LateUpdate()
    {
        PlayerPrefs.SetFloat("volume", slider.value);
        PlayerPrefs.SetFloat("volume1", AudioListener.volume);




    }


    public void ChangeMasterVolume(float masterVolume)
    {
        AudioListener.volume = masterVolume;
    }

    public void ChangeTrack(int trackNumber)
    {
        if (isOnePlaying)
        {
            musicSource.Stop();
            musicSource2.clip = musicTracks[trackNumber];
            musicSource2.Play();
            
        }
        else {

            musicSource2.Stop();
            musicSource.clip = musicTracks[trackNumber];
            musicSource.Play();
        }

        isOnePlaying = !isOnePlaying;
        
    }
}


