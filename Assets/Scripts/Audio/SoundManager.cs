using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioSource musicSource1, musicSource2, menuMusic, currentSource;
    [SerializeField] private AudioClip[] musicTracks;
    private AudioSource inGameMusic;
    //private Dictionary<int , AudioClip> BPMforTracks = new  Dictionary<int, AudioClip>();
    [SerializeField] private int[] BPMforTracks;
    //[SerializeField] public Slider slider;
    [SerializeField] float timeToFade = 1f;
    //[SerializeField] public Slider slidertoFind;
    public static SoundManager Instance;
    public Scene currentScene;
    public AudioMixerSnapshot lowPassSnapshots, normalSnapshots;
    public bool isOnePlaying, isLowPassOn, isInMenu, hasRun;
    public int currentTrackNumber, currentBPM;

    public static SoundManager instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        StopInGameMusic();
        isOnePlaying = true;
        isInMenu = false;
        currentSource = musicSource1;
        currentBPM = BPMforTracks[0];
        currentTrackNumber = 0;
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentTrackNumber != 0)
        {
            if (currentSource.pitch <= 0.9f)
            {
                ChangeTrack(--currentTrackNumber);
            }

        }

        if (currentTrackNumber != musicTracks.Length - 1)
        {
            if (currentSource.pitch >= 1.1f)
            {
                ChangeTrack(++currentTrackNumber);
            }
        }
       
       
    }

    private void StopInGameMusic()
    {
        musicSource1.Pause();
        musicSource2.Pause();
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

    public void StartGame()
    {
        musicSource1.Play();
        musicSource2.Stop();
        menuMusic.Stop();
        LowPassOff();
    }

    public void GoBackToMain()
    {
        if (!menuMusic.isPlaying)
        {
            menuMusic.Play();
        }
        musicSource1.Stop();
        musicSource2.Stop();
        musicSource1.clip = musicTracks[0];
        musicSource2.clip = musicTracks[0];
        LowPassOn();
    }

    public void Die()
    {
        menuMusic.Play();
        musicSource1.Stop();
        musicSource2.Stop();
        musicSource1.clip = musicTracks[0];
        musicSource2.clip = musicTracks[0];
        LowPassOn();
    }

    public void ToggleMusicPause()
    {
     
        if (isInMenu == false)
        {
            musicSource1.Pause();
            musicSource2.Pause();
            menuMusic.Play();
            LowPassOn();
        }
        else
        {
            musicSource1.UnPause();
            musicSource2.UnPause();
            menuMusic.Stop();
            LowPassOff();
        }
        isInMenu = !isInMenu;
    }

    public void LowPassOn()
    {
        lowPassSnapshots.TransitionTo(.001f);
    }

    public void LowPassOff()
    {
        normalSnapshots.TransitionTo(.001f);
    }
    public int GetCurrentBPM()
    {
        return currentBPM;
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
        currentBPM = BPMforTracks[trackNumber];
        if (isOnePlaying)
        {
            musicSource2.transform.SetAsFirstSibling();
            musicSource2.clip = musicTracks[trackNumber];
            currentSource = musicSource2;
            musicSource2.Play();
            while (timeElapsed < timeToFade)
            {
                musicSource2.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                musicSource1.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;

            }
            musicSource1.Stop();
            musicSource1.pitch = 1;
          
        }
        else
        {
            musicSource1.transform.SetAsFirstSibling();
            musicSource1.clip = musicTracks[trackNumber];
            currentSource = musicSource1;
            musicSource1.Play();
            while (timeElapsed < timeToFade)
            {
                musicSource1.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                musicSource2.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;

            }
            musicSource2.Stop();
            musicSource2.pitch = 1;
        }
    }
}