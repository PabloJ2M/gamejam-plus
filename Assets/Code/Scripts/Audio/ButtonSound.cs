using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource ButtonSource;

    private void Awake()
    {
        ButtonSource=GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
    }

    public void PlayButtonSound(AudioClip clip)
    {
        ButtonSource.PlayOneShot(clip);
    }
}