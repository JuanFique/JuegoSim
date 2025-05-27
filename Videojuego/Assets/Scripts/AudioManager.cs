using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public bool IsMuted()
    {
        return musicSource.mute;
    }
}
