using UnityEngine.Events;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Toggle))]
    public class Toggle_Icon : MonoBehaviour, IToggle
    {
        [SerializeField] private ActivationGroup _group;
        [SerializeField] private Sprite _switchIcon;
        [SerializeField] private bool _invert;
        [SerializeField] private UnityEvent<bool> _onValueChanged;

        public bool IsActive => _toggle.isOn;

        private Image _graphic;
        private Toggle _toggle;
        private Sprite _default;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _graphic = _toggle.graphic.GetComponent<Image>();
            _default = _graphic.sprite;
            _toggle.graphic = null;
        }
        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnPerfome);
            _group?.RegisterToggle(this);
        }
        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveAllListeners();
            _group?.UnregisterToggle(this);
        }

        public void SetRandomValue()
        {
            _invert = Random.value > 0.5f;
            _toggle.isOn = Random.value > 0.8f;
            OnPerfome(_toggle.isOn);
        }
        private void OnPerfome(bool value)
        {
            if (!_invert) _graphic.sprite = !value ? _default : _switchIcon;
            else _graphic.sprite = value ? _default : _switchIcon;
            _onValueChanged.Invoke(value);
            _group?.UpdateToggleStatus();
        }
    }
}