using UnityEngine;
using UnityEngine.Events;

namespace Player.Data
{
    public class EnergyListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent<bool> _onStatusChanged;
        private Energy _energy;

        private void Awake() => _energy = Energy.Instance;
        private void OnEnable() => _energy.onEnergyUpdated += _onStatusChanged.Invoke;
        private void OnDisable() => _energy.onEnergyUpdated -= _onStatusChanged.Invoke;
        public void OnInteract() => _energy.RemoveEnergy();
    }
}