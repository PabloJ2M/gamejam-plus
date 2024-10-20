using UnityEngine;

namespace Events.Render
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleController : MonoBehaviour
    {
        private ParticleSystem _system;
        private ParticleSystem.EmissionModule _emission;

        private void Awake()
        {
            _system = GetComponent<ParticleSystem>();
            _emission = _system.emission;
        }

        public void EnableEmission() => _emission.enabled = true;
        public void DisableEmission() => _emission.enabled = false;
    }
}