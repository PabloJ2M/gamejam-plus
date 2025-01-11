using UnityEngine.Pool;

namespace Gameplay.Runner
{
    public class RandomObstacle : ItemBehaviour
    {
        protected Spawner _spawner;

        protected override void Awake() { base.Awake(); _spawner = GetComponentInParent<Spawner>(); }
        private void LateUpdate()
        {
            if (Position.x > _spawner.Limit) return;
            _spawner.Release(this);
        }
    }
}