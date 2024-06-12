// made by NPF & AI
// don't profit from this unmodified script and don't claim it's your's
// enjoy :)

using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class AutoCloneDefaultMaterial
{
    static AutoCloneDefaultMaterial()
    {
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    static void OnHierarchyChanged()
    {
        GameObject[] rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject rootObject in rootObjects)
        {
            // Search for newly added GameObjects in the hierarchy
            GameObject[] newObjects = Selection.gameObjects;

            foreach (GameObject newObject in newObjects)
            {
                Renderer renderer = newObject.GetComponent<Renderer>();

                // Check if the newly added GameObject has a Renderer component
                if (renderer != null)
                {
                    // Check if the material is the default one
                    if (IsMaterialDefault(renderer.sharedMaterial))
                    {
                        // Clone the material and assign the cloned material to the renderer
                        Material clonedMaterial = new Material(renderer.sharedMaterial);
                        clonedMaterial.name = "CustomMaterial_" + renderer.sharedMaterial.name; // Change the name to avoid confusion
                        renderer.sharedMaterial = clonedMaterial;
                    }
                }

                // Check if the object being duplicated has a cloned material
                Renderer newRenderer = newObject.GetComponent<Renderer>();
                if (newRenderer != null)
                {
                    Material originalMaterial = newRenderer.sharedMaterial;
                    if (originalMaterial != null && originalMaterial.name.StartsWith("CustomMaterial_", System.StringComparison.Ordinal))
                    {
                        // Clone the material and assign it to the new object
                        Material clonedMaterial = new Material(originalMaterial);
                        clonedMaterial.name = "CustomMaterial_" + originalMaterial.name + "_Copy"; // Add _Copy to distinguish it from the original cloned material
                        newRenderer.sharedMaterial = clonedMaterial;
                    }
                }
            }
        }
    }

    static bool IsMaterialDefault(Material material)
    {
        // Check if the material is a default Unity material
        return material != null && material.name.StartsWith("Default-", System.StringComparison.Ordinal);
    }
}