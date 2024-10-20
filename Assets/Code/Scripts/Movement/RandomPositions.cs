using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class RandomPositions : MonoBehaviour
    {
        [SerializeField] private float _area, _threshold;
        private Transform _transform;

        private void Awake() => _transform = transform;
        //private void Start() => SetRandomPosition();

        public void SetRandomPosition()
        {
            List<Vector3> positions = new();
            for (int i = 0; i < _transform.childCount; i++)
            {
                Vector2 pos;
                bool isValid;

                do { pos = Random.insideUnitSphere * _area; isValid = IsPositionValid(positions, pos); } while (!isValid);
                _transform.position = pos;
                positions.Add(pos);
            }
        }
        private bool IsPositionValid(List<Vector3> positions, Vector3 position)
        {
            foreach (var pos in positions)
            {
                if ((pos - position).magnitude < _area)
                    return false;
            }

            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _area);
        }
    }
}