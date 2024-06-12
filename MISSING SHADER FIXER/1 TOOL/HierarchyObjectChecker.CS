// made by NPF & AI
// don't profit from this unmodified script and don't claim it's your's
// enjoy :)

using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class HierarchyObjectChecker
{
    static HierarchyObjectChecker()
    {
        EditorApplication.hierarchyChanged += CheckHierarchyObjects;
    }

    static void CheckHierarchyObjects()
    {
        // Get all GameObjects in the scene
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allGameObjects)
        {
            // Check if the object has any of the specified components
            if (obj.GetComponent<Renderer>() != null)
            {
                // If any of the specified components exist, replace the shader
                Renderer renderer = obj.GetComponent<Renderer>();
                Material[] materials = renderer.sharedMaterials;

                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i].shader.name.Contains("Hidden") ||
                        materials[i].shader.name.Contains("Internal") ||
                        materials[i].shader.name.Contains("Error") ||
                        materials[i].shader.name.Contains("Shader"))
                    {
                        // Replace the shader with Standard Shader
                        materials[i].shader = Shader.Find("Standard");
                    }
                }
            }
        }
    }
}