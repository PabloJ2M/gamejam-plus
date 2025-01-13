using UnityEngine;

public enum CraftingResources
{
   Welfare,
   Maintenance,
   Intelligence
}

public class CraftingResourcesManager : MonoBehaviour
{
   public static void AddResource(CraftingResources resourceType, float amount)
   {
       string resourceName = resourceType.ToString();
       float resource = GetResource(resourceType);

       resource = Mathf.Max(0f, resource + amount);

       PlayerPrefs.SetFloat(resourceName, resource);
   }

   public static void RemoveResource(CraftingResources resourceType, float amount)
   {
        AddResource(resourceType, -Mathf.Abs(amount));
   }

   public static float GetResource(CraftingResources resourceType)
   {
       string resourceName = resourceType.ToString();

       return PlayerPrefs.GetFloat(resourceName);
   }

   public static void LogResources()
   {
        foreach(CraftingResources resource in System.Enum.GetValues(typeof(CraftingResources)))
        {
            Debug.Log(resource.ToString() + ": " + CraftingResourcesManager.GetResource(resource));
        }
   }
}
