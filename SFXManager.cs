using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioSource[] soundEffects;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffect(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();
        soundEffects[soundToPlay].Play();
    }

    public void PlaySoundEffectPitched(int soundToPlay)
    {
        soundEffects[soundToPlay].pitch = Random.Range(0.8f, 1.2f);
        soundEffects[soundToPlay].Stop();
        soundEffects[soundToPlay].Play();
    }
}
