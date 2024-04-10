using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource1, musicSource2, ambeintSource;
    [SerializeField] private AudioClip[] musicTracks;
    [SerializeField] public Slider slider;
    [SerializeField] float timeToFade = 1f;
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
        musicSource1 = GetComponent<AudioSource>();
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
            musicSource1.volume = 0;
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
       StopAllCoroutines();
        StartCoroutine(FadeTrack(trackNumber));
        isOnePlaying = !isOnePlaying;
        
    }

    private IEnumerator FadeTrack(int trackNumber)
    {
       
        float timeElapsed = 0;

        if (isOnePlaying)
        {
            musicSource2.clip = musicTracks[trackNumber];
            musicSource2.Play();
            while (timeElapsed < timeToFade)
            {
                musicSource2.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                musicSource1.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;

            }
            musicSource1.Stop();
            

        }
        else
        {

            musicSource1.clip = musicTracks[trackNumber];
            musicSource1.Play();
            while (timeElapsed < timeToFade)
            {
                musicSource1.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                musicSource2.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;

            }
            musicSource2.Stop();
        }
      
    
    
    }
}


