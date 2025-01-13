using UnityEngine.Events;
using Unity.Mathematics;

namespace UnityEngine.UI.Display
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDeath;

        private Image _image;
        private int _maxHealth;

        private void Awake() => _image = GetComponent<Image>();
        private void Start() { _maxHealth = _health; UpdateHealth(); }

        public void AddHealth(int amount) { _health += amount; UpdateHealth(); }
        public void RemoveHealth(int amount) { _health -= amount; UpdateHealth(); }
        public void ResetHealth() { _health = _maxHealth; UpdateHealth(); }
        private void UpdateHealth()
        {
            _health = math.clamp(_health, 0, _maxHealth);
            _image.fillAmount = _health / (float)_maxHealth;
            if (_health == 0) _onDeath.Invoke();
        }
    }
}