using UnityEngine;

namespace Unity.Cinemachine
{
    public class CameraShakeEffect : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private float _amount = 1;
        [SerializeField, Range(1, 10)] private float _speed = 1;

        private CinemachineBasicMultiChannelPerlin _noise;

        private void Awake() => _noise = GetComponent<CinemachineBasicMultiChannelPerlin>();
        private void Start() => Shake(0);

        private void Update()
        {
            if (_noise.FrequencyGain == 0) return;

            _noise.FrequencyGain = Mathf.MoveTowards(_noise.FrequencyGain, 0, Time.deltaTime * _speed);
            _noise.AmplitudeGain = _noise.FrequencyGain;
        }

        [ContextMenu("Shake Camera")]
        public void Shake() => Shake(_amount);
        public void Shake(float value) => _noise.FrequencyGain = _noise.AmplitudeGain = value;
    }
}