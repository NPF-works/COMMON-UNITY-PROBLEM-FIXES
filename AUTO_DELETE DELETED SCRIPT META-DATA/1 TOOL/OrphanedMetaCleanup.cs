// made by NPF & AI
// don't profit from this unmodified script and don't claim it's your's
// enjoy :)

using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class OrphanedMetaCleanup : UnityEditor.AssetModificationProcessor
{
    static OrphanedMetaCleanup()
    {
        EditorApplication.projectChanged += OnProjectChanged;
    }

    static void OnProjectChanged()
    {
        DeleteOrphanedMetaFiles();
    }

    static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
    {
        // If a script is being deleted
        if (assetPath.EndsWith(".cs"))
        {
            string metaPath = assetPath + ".meta";
            if (File.Exists(metaPath))
            {
                File.Delete(metaPath);
                Debug.Log("Deleted orphaned meta file: " + metaPath);
                AssetDatabase.Refresh();
            }
        }
        return AssetDeleteResult.DidNotDelete;
    }

    static void DeleteOrphanedMetaFiles()
    {
        string[] metaFiles = Directory.GetFiles(Application.dataPath, "*.meta", SearchOption.AllDirectories);

        foreach (string metaFile in metaFiles)
        {
            string assetPath = metaFile.Replace(Application.dataPath, "Assets").Replace(".meta", "");

            if (!File.Exists(assetPath))
            {
                File.Delete(metaFile);
                Debug.Log("Deleted orphaned meta file: " + metaFile);
                AssetDatabase.Refresh();
            }
        }
    }
}