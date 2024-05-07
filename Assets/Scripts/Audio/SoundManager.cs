using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioSource musicSource1, musicSource2, menuMusic, sfxSource, currentSource;
    [SerializeField] private AudioClip[] musicTracks, clickSound;
    [SerializeField] private int[] BPMForTracks;
    [SerializeField] private float timeToFade = 1f;
    [SerializeField] private AudioSource sfxObject;
    

    public Scene currentScene;
    public AudioMixerSnapshot lowPassSnapshots, normalSnapshots;
    public bool isOnePlaying, isLowPassOn, isInMenu, hasRun, isTimeToChange;
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
        isTimeToChange = false;
        currentSource = musicSource1;
        currentBPM = BPMForTracks[0];
        currentTrackNumber = 0;

        UnityAction action = new UnityAction(CheckIfItsTimeToChangeTrack);
        TriggerController.Instance.SetTrigger(1, action);
    }

    void FixedUpdate()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentTrackNumber != 0)
        {
            if (Mathf.Approximately(currentSource.pitch, 0.9f))
            {

                ChangeTrack(--currentTrackNumber);
            }

        }

        if (currentTrackNumber != musicTracks.Length - 1)
        {
            if (Mathf.Approximately(currentSource.pitch, 1.1f))
            {
                ChangeTrack(++currentTrackNumber);
            }
        }

    }

    public void CheckIfItsTimeToChangeTrack()
    {
       
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
        currentBPM = BPMForTracks[trackNumber];
        if (isOnePlaying)
        {
            musicSource2.transform.SetAsFirstSibling();
            musicSource2.clip = musicTracks[trackNumber];
            currentSource = musicSource2;
            musicSource2.Play();
            while (timeElapsed <= timeToFade)
            {
                timeElapsed += Time.deltaTime;
                musicSource2.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                musicSource1.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
               
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
                timeElapsed += Time.deltaTime;
                musicSource1.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                musicSource2.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
               
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
                elapsedTime += Time.deltaTime;
                audioSource.pitch = Mathf.Lerp(currentPitch, nexPitch, elapsedTime / timeToChange);
                Debug.Log(elapsedTime / timeToChange);
               
                yield return null;

            }

        }

     
    }

    public void PlaySFX(AudioClip clip, Transform transform, float volume, int priority)
    {
        AudioSource audioSource = Instantiate(sfxObject, transform.position, Quaternion.identity);

        audioSource.clip = clip;
        audioSource.pitch = this.transform.GetChild(0).GetComponent<AudioSource>().pitch;
        audioSource.priority = priority;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);

    }

    public void Click()
    {
        sfxSource.PlayOneShot(clickSound[0], 1); 
    }

    
}