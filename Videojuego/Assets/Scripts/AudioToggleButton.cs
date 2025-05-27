using UnityEngine;
using UnityEngine.UI;

public class AudioToggleButton : MonoBehaviour
{
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Image buttonImage;
    public AudioManager audioManager;

    public void ToggleMusic()
    {
        audioManager.ToggleMusic();

        if (audioManager.IsMuted())
        {
            buttonImage.sprite = musicOffSprite;
        }
        else
        {
            buttonImage.sprite = musicOnSprite;
        }
    }
}
