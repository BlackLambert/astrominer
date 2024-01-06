using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SBaier.Astrominer.Editor
{
    public static class MissingScriptOnPrefabsFinder
    {
        [MenuItem("Tools/Utility/FindPrefabsWithMissingScripts")]
        public static void Find()
        {
            string[] prefabIds =
                AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources/Prefabs" });

            Debug.Log($"[Find prefabs with missing scripts] {prefabIds.Length} prefabs in total found");
            List<Result> results = new List<Result>();
            
            foreach (string prefabId in prefabIds)
            {
                results.AddRange(FindObjectsWithMissingScripts(prefabId));
            }
            
            Debug.Log($"[Find prefabs with missing scripts] Found the following {results.Count} prefabs with missing scripts");

            foreach (Result result in results)
            {
                Debug.Log($"[Find prefabs with missing scripts] {result}");
            }
        }

        private static List<Result> FindObjectsWithMissingScripts(string prefabId)
        {
            List<Result> result = new List<Result>();
            string path = AssetDatabase.GUIDToAssetPath(prefabId);
            GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            FindObjectsInternal(ref result, gameObject.name, gameObject, path);
            return result;
        }

        private static void FindObjectsInternal(ref List<Result> result, string parent, GameObject gameObject, string path)
        {
            Component[] components = gameObject.GetComponents<Component>();
            path += $"/{gameObject.name}";

            foreach (Component component in components)
            {
                if (component == null)
                {
                    result.Add(new Result{Parent = parent, Path = path, GameObjectName = gameObject.name});
                }
            }
            
            foreach (Transform transform in gameObject.transform)
            {
                FindObjectsInternal(ref result, parent, transform.gameObject, path);
            }
        }

        private class Result
        {
            public string Path { get; set; }
            public string GameObjectName { get; set; }
            public string Parent { get; set; }

            public override string ToString()
            {
                return $"\"{GameObjectName}\" of Prefab \"{Parent}\" ({Path})";
            }
        }
    }

}