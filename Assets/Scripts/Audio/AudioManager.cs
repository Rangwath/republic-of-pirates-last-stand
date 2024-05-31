using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance { get; private set; }

    [Range(0f, 3f)]
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private SoundsCollectionSO soundsCollectionSO;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) 
        { 
            Instance = this; 
        }
        else if (Instance != this)
        {
            Debug.LogWarning("Another instance detected and destroyed");
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Cannon.OnCannonFire += Cannon_OnCannonFire;
        Health.OnWoodImpact += Health_OnWoodImpact;
        Health.OnStoneImpact += Health_OnStoneImpact;
    }

    private void OnDisable()
    {
        Cannon.OnCannonFire -= Cannon_OnCannonFire;
        Health.OnWoodImpact -= Health_OnWoodImpact;
        Health.OnStoneImpact -= Health_OnStoneImpact;
    }

    private void PlaySoundSO(SoundSO soundSO)
    {
        AudioClip clip = soundSO.Clip;
        float pitch = soundSO.Pitch;
        float volume = soundSO.Volume * masterVolume;
        bool loop = soundSO.Loop;

        if (soundSO.RandomizePitch)
        {
            float randomPitchRangeModifier = Random.Range(-soundSO.RandomPitchRangeModifier, soundSO.RandomPitchRangeModifier);
            pitch = soundSO.Pitch + randomPitchRangeModifier;
        }

        PlaySound(clip, pitch, volume, loop);
    }

    private void PlaySound(AudioClip clip, float pitch, float volume, bool loop)
    {
        GameObject soundObject = new GameObject("Temp Audio Source");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();

        if (!loop)
        {
            Destroy(soundObject, clip.length);
        }
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
