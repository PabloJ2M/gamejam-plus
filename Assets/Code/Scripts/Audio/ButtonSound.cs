using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource ButtonSource;

    private void Awake()
    {
        GameObject audio = GameObject.FindWithTag("Audio");
        if (audio == null) { print("audio not found in scene"); return; }

        ButtonSource = audio.GetComponent<AudioSource>();
    }

    public void PlayButtonSound(AudioClip clip)
    {
        ButtonSource?.PlayOneShot(clip);
    }
}