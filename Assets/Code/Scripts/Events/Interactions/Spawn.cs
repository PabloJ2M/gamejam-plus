using UnityEngine;

namespace Events.Interact
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] private GameObject _item;

        public void SpawnObject() => Instantiate(_item, transform.position, transform.rotation);
    }
}