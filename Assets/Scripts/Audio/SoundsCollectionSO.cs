using UnityEngine;

[CreateAssetMenu()]
public class SoundsCollectionSO : ScriptableObject
{
    // SFX
    public SoundSO CannonFire;
    public SoundSO CannonBallImpactWood;
    public SoundSO CannonBallImpactStone;

    // Music
    public SoundSO MenuMusic;
    public SoundSO GameOverMusic;
}