using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    [RequireComponent(typeof(RectTransform))]
    public class RandomPositions : MonoBehaviour
    {
        [SerializeField] private float _threshold;
        private RectTransform _rectTransform;

        private void Awake() => _rectTransform = GetComponent<RectTransform>();
        private void Start() => SetRandomPosition();

        public void SetRandomPosition()
        {
            List<Vector2> positions = new();
            for (int i = 0; i < _rectTransform.childCount; i++)
            {
                Vector2 pos;
                bool isValid;

                do { pos = RandomPosition(); isValid = IsPositionValid(positions, pos); } while (!isValid);
                _rectTransform.GetChild(i).localPosition = pos;
                positions.Add(pos);
            }
        }
        private Vector2 RandomPosition()
        {
            Vector2 position = _rectTransform.anchoredPosition;
            float width = _rectTransform.rect.width / 2;
            float height = _rectTransform.rect.height / 2;

            position.x += Random.Range(-width, width);
            position.y += Random.Range(-height, height);
            return position;
        }
        private bool IsPositionValid(List<Vector2> positions, Vector2 position)
        {
            foreach (var pos in positions) { if ((pos - position).magnitude < _threshold) return false; }
            return true;
        }
    }
}