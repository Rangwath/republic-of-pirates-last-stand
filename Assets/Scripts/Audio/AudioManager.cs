using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance { get; private set; }

    [Range(0f, 3f)]
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float fadeMusicTime = 1f;
    [SerializeField] private SoundsCollectionSO soundsCollectionSO;

    private AudioSource currentMusic;
    private bool isMusicPlaying = false;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) 
        { 
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += StopCurrentMusic;
        GameManager.OnGameOver += GameManager_OnGameOver;

        MainMenuManager.OnMainMenuStarted += MainMenuManager_OnMainMenuStarted;

        Cannon.OnCannonFire += Cannon_OnCannonFire;
        
        Hitable.OnWoodHit += Health_OnWoodImpact;
        Hitable.OnStoneHit += Health_OnStoneImpact;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= StopCurrentMusic;
        GameManager.OnGameOver -= GameManager_OnGameOver;

        MainMenuManager.OnMainMenuStarted -= MainMenuManager_OnMainMenuStarted;

        Cannon.OnCannonFire -= Cannon_OnCannonFire;
        
        Hitable.OnWoodHit -= Health_OnWoodImpact;
        Hitable.OnStoneHit -= Health_OnStoneImpact;
    }

    private void PlaySoundSO(SoundSO soundSO)
    {
        AudioClip clip = soundSO.Clip;
        float pitch = soundSO.Pitch;
        float volume = soundSO.Volume * masterVolume;
        bool loop = soundSO.Loop;
        bool music = soundSO.Music;

        if (soundSO.RandomizePitch)
        {
            float randomPitchRangeModifier = Random.Range(-soundSO.RandomPitchRangeModifier, soundSO.RandomPitchRangeModifier);
            pitch = soundSO.Pitch + randomPitchRangeModifier;
        }

        PlaySound(clip, pitch, volume, loop, music);
    }

    private void PlaySound(AudioClip clip, float pitch, float volume, bool loop, bool music)
    {
        GameObject soundObject = new GameObject("Temp Audio Source");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.loop = loop;

        if (!loop)
        {
            Destroy(soundObject, clip.length);
        }

        if (music)
        {
            if (isMusicPlaying)
            {
                StopCurrentMusic();
            }
            StartCoroutine(StartNewMusicRoutine(soundObject, audioSource));
        }
        else
        {
            audioSource.Play();
        }
    }

    private void StopCurrentMusic()
    {
        StartCoroutine(FadeOutMusicRoutine());
    }

    private IEnumerator FadeOutMusicRoutine()
    {
        float elapsedTime = 0f;
        float startVolume = currentMusic.volume;

        while (elapsedTime < fadeMusicTime)
        {
            elapsedTime += Time.deltaTime;
            currentMusic.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeMusicTime);
            yield return null;
        }

        currentMusic.Stop();
        Destroy(currentMusic.gameObject);
        isMusicPlaying = false;
    }

    private IEnumerator StartNewMusicRoutine(GameObject soundObject, AudioSource audioSource)
    {
        // Wait until the old music fades out
        while (isMusicPlaying)
        {
            yield return null;
        }

        currentMusic = audioSource;
        currentMusic.Play();
        isMusicPlaying = true;

        soundObject.name = "Music Audio Source";
        DontDestroyOnLoad(soundObject);
    }

    private void MainMenuManager_OnMainMenuStarted()
    {
        PlaySoundSO(soundsCollectionSO.MenuMusic);
    }

    private void GameManager_OnGameOver()
    {
        PlaySoundSO(soundsCollectionSO.GameOverMusic);
    }

    private void Cannon_OnCannonFire()
    {
        PlaySoundSO(soundsCollectionSO.CannonFire);
    }

    private void Health_OnWoodImpact()
    {
        PlaySoundSO(soundsCollectionSO.CannonBallImpactWood);
    }

    private void Health_OnStoneImpact()
    {
        PlaySoundSO(soundsCollectionSO.CannonBallImpactStone);
    }
}
