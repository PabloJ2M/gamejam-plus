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
            Timeout.SaveExpirationDate(_id);
        }
        private void Start()
        {
            int days = Timeout.GetExpirationLengthDays(_id);
            _onDateUpdated.Invoke($"Días: {days}");
        }
    }
}