using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioSource musicSource1, musicSource2, menuMusic, currentSource, sfxSource;
    [SerializeField] private AudioClip[] musicTracks, clickSound;
    [SerializeField] private int[] BPMforTracks;
    [SerializeField] float timeToFade = 1f;
    

    public Scene currentScene;
    public AudioMixerSnapshot lowPassSnapshots, normalSnapshots;
    public bool isOnePlaying, isLowPassOn, isInMenu, hasRun;
    public int currentTrackNumber, currentBPM;

    [Header("Beat relateted")]
    [SerializeField] private float MusicSpeedChange;
    [SerializeField] private float timeToChange;

    public static SoundManager Instance
    {
        get; private set;
    }

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
        musicSource1.pitch = 1;
        musicSource2.pitch = 1;
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
            while (timeElapsed <= timeToFade)
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
            while (timeElapsed <= timeToFade)
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

    public void ChangePitch(bool increasePitch)
    {
        StartCoroutine(ChangePitchCoroutine(increasePitch));
        
    }

    private  IEnumerator ChangePitchCoroutine(bool increasePitch)
    {
        AudioSource audioSource = transform.GetChild(0).GetComponent<AudioSource>();
        int direction = increasePitch ? 1 : -1;
        float elapsedTime = 0;
        float nexPitch = audioSource.pitch + (MusicSpeedChange * direction);
        float currentPitch = audioSource.pitch;
       

        if (audioSource.pitch <= 0.5)
        {
            audioSource.pitch = 0.5f;
        }
        else
        {
            while (elapsedTime <= timeToChange)
            {
               
                audioSource.pitch = Mathf.Lerp(currentPitch, nexPitch, elapsedTime / timeToChange);
                elapsedTime += Time.deltaTime;
                yield return null;

            }

        }

     
    }

    public void Click()
    {
        sfxSource.PlayOneShot(clickSound[0], 1); 
    }

    
}