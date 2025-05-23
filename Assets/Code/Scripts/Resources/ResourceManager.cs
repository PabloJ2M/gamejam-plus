using System;
using UnityEngine;

namespace Player.Data
{
    public enum ResourceType { Coins, Hearts }

    public class ResourceManager : MonoBehaviour
    {
        public static event Action onResourcesUpdated;

        public static Vector3 GetResource() => 
            new (GetResource(ResourceType.Hearts), GetResource(ResourceType.Coins));

        public static float GetResource(ResourceType type) => PlayerPrefs.GetFloat(type.ToString());
        public static void RemoveResource(ResourceType type, float amount) => AddResource(type, -Mathf.Abs(amount));
        public static void AddResource(ResourceType type, float amount)
        {
            float resource = GetResource(type);
            resource = Mathf.Max(0f, resource + amount);
            PlayerPrefs.SetFloat(type.ToString(), resource);

            onResourcesUpdated.Invoke();
        }

        public static void LogResources()
        {
            foreach(ResourceType resource in Enum.GetValues(typeof(ResourceType)))
                print($"{resource} {GetResource(resource)}");
        }
    }
}