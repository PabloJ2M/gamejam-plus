using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [System.Serializable]
    public struct AudioProfile
    {
        public string channelName;
        public Slider optionSlider;
        public float maxVolume;
        public float zeroVolume;
    }

    [Header("Audio Components")]
    [SerializeField] private AudioMixer _mixer;

    [Header("Profiles")]
    [SerializeField] private List<AudioProfile> _audioProfiles;

    private void Awake()
    {
        _audioProfiles.ForEach(a => SetUpdateAudioChannel(a));
    }

    private void Start()
    {
        _audioProfiles.ForEach(a => UpdateAudioChannel(a));
    }

    private void SetUpdateAudioChannel(AudioProfile _audioProfile)
    {
        _audioProfile.optionSlider.onValueChanged.AddListener(s => UpdateAudioChannel(_audioProfile));

        UpdateAudioChannel(_audioProfile);

    }

    private void UpdateAudioChannel(AudioProfile _audioProfile)
    {
        float maxVolume = _audioProfile.maxVolume;
        float zeroVolume = _audioProfile.zeroVolume;
        float sliderValue = _audioProfile.optionSlider.value;
        float volumeValue = ValueToVolume(sliderValue, maxVolume, zeroVolume);

        _mixer.SetFloat(_audioProfile.channelName, volumeValue);
    }

    private float ValueToVolume(float value, float maxVolume, float zeroVolume)
    {
        return Mathf.Log10(value) * (maxVolume - zeroVolume) / 4 + maxVolume;
    }
}
