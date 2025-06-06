using UnityEngine;

namespace Events.Physic
{
    public static class LayerExtension
    {
        public static bool CompareLayer(this LayerMask layerMask, GameObject gameObject)
        {
            return (layerMask.value & (1 << gameObject.layer)) > 0;
        }
    }
}