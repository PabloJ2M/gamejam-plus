using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.GuitarHero
{
    [CreateAssetMenu(fileName = "beat data", menuName = "system/audio/beats")]
    public class BeatDataScriptable : ScriptableObject
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private List<float> _beatTimes = new();
        
        public int Count => _beatTimes.Count;
        public float Length => _clip.length;

        public AudioClip Clip => _clip;
        public List<float> BeatTimes { get => _beatTimes; set => _beatTimes = value; }
    }
}