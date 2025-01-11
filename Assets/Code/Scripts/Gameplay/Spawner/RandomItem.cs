using UnityEngine;

namespace Gameplay.Runner
{
    public class RandomItem : RandomObstacle
    {
        private void Start() => Index = -1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            _spawner?.OnCollectScore();
            _spawner.Release(this);
        }
    }
}