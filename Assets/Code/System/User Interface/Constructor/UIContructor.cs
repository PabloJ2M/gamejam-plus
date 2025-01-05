using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Toulouse.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public abstract class UIController : MonoBehaviour
    {
        [SerializeField] private UIContructorItem _prefab;
        protected List<UIContructorItem> _uiItems = new();
        private ScrollRect _scrollRect = null;

        protected virtual void Awake() => _scrollRect = GetComponent<ScrollRect>();
        private void OnEnable() => DisplayItems();

        public abstract void DisplayItems();
        protected virtual void ObjectPulling(int index, object data)
        {
            if (index >= _scrollRect.content.childCount) _uiItems.Add(Instantiate(_prefab, _scrollRect.content));
            if (index >= _uiItems.Count) _uiItems.Add(_scrollRect.content.GetChild(index).GetComponent<UIContructorItem>());

            _uiItems[index].gameObject.SetActive(true);
            _uiItems[index].Setup(data);
        }
    }

    [RequireComponent(typeof(ScrollRect))]
    public abstract class UIContructor<T> : MonoBehaviour where T : ScriptableObject
    {
        [SerializeField] private UIContructorItem<T> _prefab;
        protected List<UIContructorItem<T>> _uiItems = new();
        private ScrollRect _scrollRect = null;

        protected virtual void Awake() => _scrollRect = GetComponent<ScrollRect>();
        protected virtual void OnEnable() => DisplayItems();

        public abstract void DisplayItems();
        protected virtual void ObjectPulling(int index, T item, bool isOwned)
        {
            if (index >= _scrollRect.content.childCount) _uiItems.Add(Instantiate(_prefab, _scrollRect.content));
            if (index >= _uiItems.Count) _uiItems.Add(_scrollRect.content.GetChild(index).GetComponent<UIContructorItem<T>>());

            _uiItems[index].gameObject.SetActive(true);
            _uiItems[index].Setup(item, isOwned);
        }
    }
}