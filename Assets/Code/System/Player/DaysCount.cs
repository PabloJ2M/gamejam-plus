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
            PlayerPrefs.SetString(_id, DateTime.Now.ToLongDateString());
        }
        private void Start()
        {
            string data = PlayerPrefs.GetString(_id);
            DateTime.TryParse(data, out DateTime time);
            TimeSpan result = DateTime.Now.Subtract(time);

            _onDateUpdated.Invoke($"Días: {result.Days}");
        }
    }
}