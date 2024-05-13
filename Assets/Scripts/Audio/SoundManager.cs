using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioSource musicSource1, musicSource2, menuMusic, SFXSource, currentSource;
    [SerializeField] private AudioClip[] musicTracks, clickSound;
    [SerializeField] private int[] BPMForTracks;
    [SerializeField] private float timeToFade = 1f;
    [SerializeField] private AudioSource sfxObject;
    

    public Scene currentScene;
    public AudioMixerSnapshot lowPassSnapshots, normalSnapshots;
    public bool isOnePlaying, isLowPassOn, isInMenu, hasRun, isTimeToChange;
    public int currentTrackNumber, currentBPM, currentPitchAdjustedBPM;


    [Header("Beat relateted")]
    [SerializeField] private float timeToChange;
    [SerializeField] private int beatThreshold;
    // how many beats does it take to change track
    private int totalPitchChange; // how many times pitch has changed

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

    public void DestroyInstance()
    {
        Destroy(gameObject);
        Instance = null;
    }

    void Start()
    {
        StopInGameMusic();
        isOnePlaying = true;
        isInMenu = false;
        isTimeToChange = false;
        currentSource = musicSource1;
        currentBPM = BPMForTracks[0];
        currentPitchAdjustedBPM = currentBPM;
        currentTrackNumber = 0;
        totalPitchChange = 0;
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

        if (currentTrackNumber != 0)
        {
            if (totalPitchChange == -beatThreshold)
            {

                ChangeTrack(--currentTrackNumber);
            }

        }

        if (currentTrackNumber != musicTracks.Length - 1)
        {
            if (totalPitchChange == beatThreshold)
            {
                ChangeTrack(++currentTrackNumber);
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
        StartCoroutine(FadeTrack(trackNumber));
        isOnePlaying = !isOnePlaying;
    }

    private IEnumerator FadeTrack(int trackNumber)
    {
        float timeElapsed = 0;
        currentBPM = BPMForTracks[trackNumber];
        totalPitchChange = 0;
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
        StartCoroutine(UpdateMusicSpeedSliderValue());
      
    }

    private IEnumerator ChangePitchCoroutine(bool increasePitch)
    {
        float MusicSpeedChange = (10 / currentBPM) / (float)beatThreshold; //  calculate the percentage increase or decrease from current track to the next or las track
        int direction = increasePitch ? 1 : -1;
        currentPitchAdjustedBPM += direction * (10 / beatThreshold);
        totalPitchChange += (direction * 1);
        float elapsedTime = 0;
        float nexPitch = currentSource.pitch + (MusicSpeedChange * direction);
        float currentPitch = currentSource.pitch;
       
       
        while (elapsedTime <= timeToChange)
        {
            elapsedTime += Time.deltaTime;
            currentSource.pitch = Mathf.Lerp(currentPitch, nexPitch, elapsedTime / timeToChange);
           
            yield return null;
        }  
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
        audioSource.pitch = currentSource.pitch;
        //audioSource.volume = (float)UnityEngine.Random.Range(0.5f, volume);
        audioSource.PlayOneShot(clip, volume);

    }

    public void PlayEnemySFX(GameObject gameobject, AudioClip clip, float volume)
    {
        Debug.Log(canPlay);
        if (canPlay)
        {
            AudioSource audioSource = gameobject.GetComponent<AudioSource>();
            audioSource.pitch = currentSource.pitch;
            //audioSource.volume = (float)UnityEngine.Random.Range(0.5f, volume);
            audioSource.PlayOneShot(clip, volume);
        }

    }

    public void Click()
    {
        float randomPitch = UnityEngine.Random.Range(-0.2f, 0.2f);
        SFXSource.pitch = Mathf.Clamp(SFXSource.pitch + randomPitch, 0.1f, 3.0f);
        SFXSource.PlayOneShot(clickSound[0], 1); 
    }

    
}