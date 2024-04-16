using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    
    [SerializeField] public AudioSource musicSource1, musicSource2, menuMusic;
    [SerializeField] private AudioClip[] musicTracks;
    private AudioSource inGameMusic;
    [SerializeField] public int[] BPMforTracks;
    //[SerializeField] public Slider slider;
    [SerializeField] float timeToFade = 1f;
    //[SerializeField] public Slider slidertoFind;
    public static SoundManager Instance;
    public static int sliderValue;
    public Scene currentScene;
    public AudioMixerSnapshot lowPassSnapshots, normalSnapshots;
    public bool isOnePlaying, isLowPassOn, isInMainMenu, hasRun;



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
        //slider = GameObject.FindGameObjectWithTag("volumeSlider").GetComponent<Slider>();
        inGameMusic = transform.GetChild(0).GetComponent<AudioSource>();

    }


    void Start()
    {
        musicSource1.Pause();
        musicSource2.Pause();




        isOnePlaying = true;
        isLowPassOn = true;
        //menuMusic.Play();
       
        Debug.Log("work");


        //AudioListener.volume = PlayerPrefs.GetFloat("volume1");
        //slider.value = PlayerPrefs.GetFloat("volume");
        //SoundMannerger.Instance.ChangeMasterVolume(slider.value);
        //slider.onValueChanged.AddListener(val => SoundMannerger.Instance.ChangeMasterVolume(val));
        //AudioListener.volume = PlayerPrefs.GetFloat("volume1");
        //slider.value = PlayerPrefs.GetFloat("volume");
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
       



        //slider = GameObject.FindGameObjectWithTag("volumeSlider").GetComponent<Slider>();

        //slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
        //SoundManager.Instance.ChangeMasterVolume(slider.value);



    }

     void LateUpdate()
     {
        //PlayerPrefs.SetFloat("volume", slider.value);
        //PlayerPrefs.SetFloat("volume1", AudioListener.volume);




     }

    public void ToggleInGameMusic()
    {
        if(hasRun != !hasRun)
        {
            musicSource1.Play();
            musicSource2.Play();
        }
        else
        {
            musicSource1.Stop();
            musicSource2.Stop();
        }
        isLowPassOn = !isLowPassOn;

    }

    public void ToggleMusicPause()
    {
        if (!isLowPassOn)
        {
            musicSource1.Pause();
            musicSource2.Pause();
            menuMusic.Play();
            lowPassSnapshots.TransitionTo(.001f);
        }
        else
        {
            musicSource1.UnPause();
            musicSource2.UnPause();
            menuMusic.Stop();
            normalSnapshots.TransitionTo(.001f);
        }
        isLowPassOn = !isLowPassOn;
        //Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void LowPassOn()
    {
        lowPassSnapshots.TransitionTo(.001f);

    }

    public void LowPassOff()
    {
        normalSnapshots.TransitionTo(.001f);
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
            musicSource2.GetComponent<Transform>().SetAsFirstSibling();
            musicSource2.GetComponent<AudioSource>().clip = musicTracks[trackNumber];

            musicSource2.GetComponent<AudioSource>().Play();
            while (timeElapsed < timeToFade)
            {
                musicSource2.GetComponent<AudioSource>().volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                musicSource1.GetComponent<AudioSource>().volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;

            }
            musicSource1.GetComponent<AudioSource>().Stop();
            Debug.Log("musicSource1 Stop");
            

        }
        else
        {
            musicSource1.GetComponent<Transform>().SetAsFirstSibling();
            musicSource1.GetComponent<AudioSource>().clip = musicTracks[trackNumber];
            musicSource1.GetComponent<AudioSource>().Play();
            while (timeElapsed < timeToFade)
            {
                musicSource1.GetComponent<AudioSource>().volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                musicSource2.GetComponent<AudioSource>().volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;

            }
            musicSource2.GetComponent<AudioSource>().Stop();
        }
      
    
    
    }
}


