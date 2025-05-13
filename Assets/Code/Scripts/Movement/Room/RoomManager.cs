using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Gameplay.Movement
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private NavMeshAgentCore _player;
        [SerializeField] private Blink _tramsition;

        [SerializeField] private CinemachineCamera _vCamera;
        private CinemachineConfiner2D _confiner;
        private WaitUntil _wait;

        public bool Success { private get; set; }

        private void Awake() => _wait = new(() => Success);
        private void Start() => _confiner = _vCamera.GetComponent<CinemachineConfiner2D>();

        public void ChangeRoom(Room room) => StartCoroutine(SetRoom(room));
        private IEnumerator SetRoom(Room area)
        {
            _tramsition.FadeIn(); Success = false;
            _vCamera.enabled = false;
            yield return _wait;

            _player.Position(area.Position);
            _confiner.BoundingShape2D = area.Area;
            _confiner.InvalidateBoundingShapeCache();

            yield return null;
            _vCamera.enabled = true;
        }
    }
}