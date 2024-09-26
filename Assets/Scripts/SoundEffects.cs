using UnityEngine;
using UnityEngine.UI;

public class SoundEffects : MonoBehaviour
{
    public Toggle music, sound;
    public AudioSource bgSound, inGameSound;
    public AudioClip buttonSound, sphereSound;

    public void PlayInGameAudio()
    {
        inGameSound.PlayOneShot(buttonSound);
    }

    public void PlaySphereAudio()
    {
        inGameSound.PlayOneShot(sphereSound);
    }

    public void ToggleMethodForMusic()
    {
        bgSound.enabled = music.isOn;
    }

    public void ToggleMethodForSounds()
    {
        inGameSound.enabled = sound.isOn;
    }
}
