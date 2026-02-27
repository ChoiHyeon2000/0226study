using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSFX;

    public void Awake()
    {
        instance = this;
    }

    public void PlaySFX(AudioClip audioClip) 
    {
        if (audioClip != null) 
        {
            return;
        }

        audioSFX.PlayOneShot(audioClip);
    }
}
