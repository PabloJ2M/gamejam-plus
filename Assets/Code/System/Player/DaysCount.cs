using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player.Data
{
    public class DaysCount : MonoBehaviour
    {
        [SerializeField] private UnityEvent<string> _onDateUpdated;
        private const string _id = "days_count";

        private void Awake()
        {
            if (PlayerPrefs.HasKey(_id)) return;
            PlayerPrefs.SetString(_id, DateTime.Now.ToString("yyyy-MM-dd"));
        }
        private void Start()
        {
            string data = PlayerPrefs.GetString(_id);
            DateTime time = DateTime.ParseExact(data, "yyyy-MM-dd", null);

            _onDateUpdated.Invoke($"Días: {(DateTime.Now - time).Days}");
        }
    }
}