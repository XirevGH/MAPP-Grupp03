using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioSource musicSource1, musicSource2, menuMusic, SFXSource, currentTrack;
    [SerializeField] private AudioClip[] musicTracks, clickSound;
    [SerializeField] private int[] BPMForTracks;
    [SerializeField] private float timeToFade = 1f;
    [SerializeField] private AudioSource sfxObject;


    public Scene currentScene;
    public AudioMixerSnapshot lowPassSnapshots, normalSnapshots;
    public bool isOnePlaying, isLowPassOn, isInMenu, hasRun, isTimeToChange;
    public int currentTrackNumber, currentBPM;
    public float currentPitchAdjustedBPM;

    [Header("Beat relateted")]
    [SerializeField] private float timeToChange;
    [SerializeField] private int beatThreshold;
    [SerializeField] private float BPMBetweenTracks;
    [SerializeField] private float maxBPM;
    [SerializeField] private float minBPM;
    // how many beats does it take to change track

    [Header("Enemy relateted")]
    [SerializeField] private int maxEnemySounds = 3;
    [SerializeField] private int currentEnemySoundsPlaying = 0;
    [SerializeField] private bool canPlay = false;

    private Slider musicSpeedSilder;
    public static SoundManager Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //slider = GameObject.FindGameObjectWithTag("volumeSlider").GetComponent<Slider>();
    }

    void Start()
    {
        StopInGameMusic();
        isOnePlaying = true;
        isInMenu = false;
        isTimeToChange = false;
        currentTrack = musicSource1;
        currentBPM = BPMForTracks[0];
        currentPitchAdjustedBPM = currentBPM;
        currentTrackNumber = 0;
      
    }

    void FixedUpdate()
    {
        currentScene = SceneManager.GetActiveScene();
        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (musicSpeedSilder == null)
            {
                musicSpeedSilder = GameObject.FindGameObjectWithTag("MusicSpeedlider").GetComponent<Slider>();
            }
        }

    }

    private void Update()
    {
        currentEnemySoundsPlaying = 0;
 
        foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>(false))
        {
            if (enemy.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                currentEnemySoundsPlaying++;
            }
        }

        canPlay = currentEnemySoundsPlaying < maxEnemySounds;

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
        normalSnapshots.TransitionTo(1f);
        //menuMusic.Stop();

        musicSource1.pitch = 1;
        musicSource2.pitch = 1;
    }

    public void GoBackToMain()
    {
        lowPassSnapshots.TransitionTo(1f);
        if (!menuMusic.isPlaying)
        {
            menuMusic.Play();
        }
        musicSource1.Stop();
        musicSource2.Stop();
        musicSource1.clip = musicTracks[0];
        musicSource2.clip = musicTracks[0];
       
    }

    public void Die()
    {
        menuMusic.Play();
        lowPassSnapshots.TransitionTo(1f);
        musicSource1.Stop();
        musicSource2.Stop();
        musicSource1.clip = musicTracks[0];
        musicSource2.clip = musicTracks[0];
       
    }

    public void ToggleMusicPause(bool state)
    {
        isInMenu = state;
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
    }

    public void LowPassOn()
    {
        lowPassSnapshots.TransitionTo(0.001f);
    }

    public void LowPassOff()
    {
        normalSnapshots.TransitionTo(0.001f);
    }

    public int GetCurrentBPM()
    {
        return currentBPM;
    }



    public void ChangeTrack(int trackNumber)
    {
        AudioClip nextTrack =  musicTracks[trackNumber];
        StartCoroutine(FadeTrack(nextTrack));
        currentBPM = BPMForTracks[trackNumber];
        isOnePlaying = !isOnePlaying;
    }

    public void ChangeTrack(int trackNumber, float pitch)
    {
        ChangeTrack(trackNumber);
        currentTrack.pitch = pitch;
    }

    private IEnumerator FadeTrack(AudioClip track)
    {
        float timeElapsed = 0;
      
       
        if (isOnePlaying)
        {
            musicSource2.transform.SetAsFirstSibling();
            musicSource2.clip = track;
            currentTrack = musicSource2;
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
            musicSource1.clip = track;
            currentTrack = musicSource1;
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

    private void UpdateTrackIfBPMChanged()
    {
        //pitch goes down

        if(Array.IndexOf(BPMForTracks, currentBPM) != 0)
        {
            if (currentPitchAdjustedBPM == currentBPM - (BPMBetweenTracks / (float)beatThreshold))
            {
                ChangeTrack(Array.IndexOf(BPMForTracks, currentBPM) - 1, 1 + (((float)BPMBetweenTracks / (float)currentBPM / (float)beatThreshold)) * (beatThreshold - 1));
            }
        }
        


        //pitch goes up
        if (Array.IndexOf(BPMForTracks, currentBPM) != BPMForTracks.Length-1)
        {
           
            if (currentPitchAdjustedBPM == BPMForTracks[Array.IndexOf(BPMForTracks, currentBPM) + 1])
            {
                ChangeTrack(Array.IndexOf(BPMForTracks, currentBPM) +1);
            }
        }
       

        
    }

  

    public void ChangePitch(bool increasePitch)
    {
        StartCoroutine(ChangePitchCoroutine(increasePitch));
        StartCoroutine(UpdateMusicSpeedSliderValue());
    }

    private IEnumerator ChangePitchCoroutine(bool increasePitch)
    {
        float elapsedTime = 0;
        float currentPitch = currentTrack.pitch;
        float nexPitch = CalculatePitchChange(increasePitch);

        while (elapsedTime <= timeToChange)
        {
            elapsedTime += Time.deltaTime;
            currentTrack.pitch = Mathf.Lerp(currentPitch, nexPitch, elapsedTime / timeToChange);
           
            yield return null;
        }

        UpdateTrackIfBPMChanged();
    }

    private float CalculatePitchChange(bool increasePitch)
    {

        float musicSpeedChange = ((float)BPMBetweenTracks / (float)currentBPM / (float)beatThreshold);
        int direction = increasePitch ? 1 : -1;
        currentPitchAdjustedBPM += (float)direction * ((float)BPMBetweenTracks / (float)beatThreshold);
        float nextPitch = currentTrack.pitch + (musicSpeedChange * direction);

        if (currentPitchAdjustedBPM > maxBPM)
        {
            currentPitchAdjustedBPM = maxBPM;
            return currentTrack.pitch;
        }

        if (currentPitchAdjustedBPM < minBPM)
        {
            currentPitchAdjustedBPM = minBPM;
            return currentTrack.pitch;
        }

        return nextPitch;
    }

    private IEnumerator UpdateMusicSpeedSliderValue()
    {
        float elapsedTime = 0;
        float currentValue = musicSpeedSilder.value;
        float nextValue = currentPitchAdjustedBPM;

        while (elapsedTime <= timeToChange)
        {
            elapsedTime += Time.deltaTime;
            musicSpeedSilder.value = Mathf.Lerp(currentValue, nextValue, elapsedTime / timeToChange);
            yield return null;
        }
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        AudioSource audioSource = SFXSource;
        audioSource.pitch = currentTrack.pitch;
        //audioSource.volume = (float)UnityEngine.Random.Range(0.5f, volume);
        audioSource.PlayOneShot(clip, volume);

    }

    public void PlayEnemySFX(GameObject gameobject, AudioClip clip, float volume)
    {
        Debug.Log(canPlay);
        if (canPlay)
        {
            AudioSource audioSource = gameobject.GetComponent<AudioSource>();
            audioSource.pitch = (float)UnityEngine.Random.Range(0.5f, currentTrack.pitch);
            audioSource.volume = (float)UnityEngine.Random.Range(volume - 0.3f, volume);
            audioSource.PlayOneShot(clip, volume);
        }

    }

    public void Click()
    {
        //float randomPitch = UnityEngine.Random.Range(-0.2f, 0.2f);
        //SFXSource.pitch = Mathf.Clamp(SFXSource.pitch + randomPitch, 0.1f, 3.0f);

        SFXSource.pitch = (float)UnityEngine.Random.Range(0.5f, 1.5f);
        SFXSource.PlayOneShot(clickSound[0], 1); 
    }

    
}