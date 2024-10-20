using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Toulouse.UI
{
    [Serializable] public struct CostView
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private TextMeshProUGUI _text;

        public void Display(bool value) => _container.gameObject.SetActive(value);
        public void SetText(int value) => _text.SetText($"{value}");
    }

    public abstract class UIContructorItem : MonoBehaviour
    {
        public abstract void Setup(object data);
    }
    public abstract class UIContructorItem<T> : MonoBehaviour where T : ScriptableObject
    {
        [SerializeField] protected Button _button;
        protected bool _isOwned;
        protected T _item;

        private void OnEnable() => _button.onClick.AddListener(Interaction);
        private void OnDisable() => _button.onClick.RemoveAllListeners();

        protected abstract void Interaction();
        public virtual void Setup(T item, bool isOwned)
        {
            _isOwned = isOwned;
            _item = item;
        }
    }
}