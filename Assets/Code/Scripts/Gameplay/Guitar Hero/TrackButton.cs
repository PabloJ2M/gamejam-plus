using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Gameplay.GuitarHero
{
    [RequireComponent(typeof(Selectable))]
    public class TrackButton : TouchBehaviour
    {
        [SerializeField] private float _threshold = 1;
        [SerializeField] private UnityEvent<float, float> _onPressed;

        private Selectable _selectable;
        private Transform _transform;

        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
            _selectable = GetComponent<Selectable>();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, new(2, _threshold * 0.75f));
        }

        public void ForceSelection(bool value)
        {
            if (value) OnSelect();
            else OnDeselect();
        }

        protected override void OnSelect()
        {
            _selectable.Select();
            _onPressed.Invoke(_transform.position.y, _threshold * 0.5f);
        }
        protected override void OnDeselect()
        {
            if (_system.currentSelectedGameObject != gameObject) return;
            _system.SetSelectedGameObject(null);
        }
    }
}