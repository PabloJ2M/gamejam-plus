using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour

{
    private AudioSource ButtonSource;
    void awake()
    {
        ButtonSource=GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
    }

    public void PlayButtonSound(AudioClip clip)
    {
        ButtonSource.PlayOneShot(clip);
    }
}
