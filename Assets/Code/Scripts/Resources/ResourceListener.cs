using System;
using Player.Data;
using TMPro;

namespace UnityEngine.UI.Display
{
    [Serializable] public struct DisplayUI
    {
        [SerializeField] private ResourceType _type;
        [SerializeField] private GameObject _object;
        [SerializeField] private TextMeshProUGUI _text;

        public ResourceType Type => _type;
        public void SetActive(bool value) => _object.SetActive(value);
        public void SetText(int value) => _text?.SetText(value.ToString());
    }

    public class ResourceListener : MonoBehaviour
    {
        [SerializeField] private DisplayUI[] _ui;

        private void OnEnable() => ResourceManager.onResourcesUpdated += Start;
        private void OnDisable() => ResourceManager.onResourcesUpdated -= Start;

        private void Start()
        {
            foreach (var element in _ui)
            {
                int amount = (int)ResourceManager.GetResource(element.Type);
                element.SetText(amount);
            }
        }
    }
}