using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Data
{
    public class Energy : SingletonBasic<Energy>
    {
        [SerializeField] private float _totalEnergy;
        [SerializeField] private int _energyCost;
        [SerializeField] private int _energyPerMinute;

        private Scrollbar _bar;
        private const string _id = "energy";
        private readonly WaitForSecondsRealtime _minute = new(60);

        public Action<bool> onEnergyUpdated;

        protected override void Awake()
        {
            base.Awake();
            _bar = GetComponent<Scrollbar>();
            _bar.size = PlayerPrefs.GetInt(_id, (int)_totalEnergy) / _totalEnergy;
        }

        private void Start()
        {
            if (Timeout.GetExpirationLeftSeconds(_id) > 0) return;

            _bar.size += (_energyPerMinute * Timeout.GetExpirationLengthMinutes(_id)) / _totalEnergy;
            OnUpdateEnergy();

            StartCoroutine(Timeout.TimeStep(_minute, true, AddEnergy));
            Timeout.SaveExpirationDate(_id);
        }
        private void OnUpdateEnergy()
        {
            onEnergyUpdated.Invoke(_bar.size <= 0);
            PlayerPrefs.SetInt(_id, (int)(_bar.size * _totalEnergy));
        }

        private void AddEnergy()
        {
            Timeout.SaveExpirationDate(_id);
            _bar.size += _energyPerMinute / _totalEnergy;
            OnUpdateEnergy();
        }
        public void RemoveEnergy()
        {
            _bar.size -= _energyCost / _totalEnergy;
            OnUpdateEnergy();
        }
    }
}