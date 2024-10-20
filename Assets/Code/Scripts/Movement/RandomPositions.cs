using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class RandomPositions : MonoBehaviour
    {
        [SerializeField] private Vector2 _area;
        [SerializeField] private float _threshold;
        private Transform _transform;

        private void Awake() => _transform = transform;
        private void Start() => SetRandomPosition();

        public void SetRandomPosition()
        {
            List<Vector3> positions = new();
            for (int i = 0; i < _transform.childCount; i++)
            {
                Vector2 pos;
                bool isValid;

                do { pos = RandomPosition(); isValid = IsPositionValid(positions, pos); } while (!isValid);
                _transform.GetChild(i).position = pos;
                positions.Add(pos);
            }
        }
        private Vector2 RandomPosition() => new Vector2(Random.Range(-_area.x, _area.x), Random.Range(-_area.y, _area.y));
        private bool IsPositionValid(List<Vector3> positions, Vector3 position)
        {
            foreach (var pos in positions) { if ((pos - position).magnitude < _threshold) return false; }
            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, _area * 2);
        }
    }
}