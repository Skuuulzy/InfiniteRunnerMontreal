using System.Collections.Generic;
using Components.Data;
using UnityEngine;

namespace Components.SODB
{
    public static class ScriptableObjectDataBase
    {
        private static readonly Dictionary<string, SOLevelParameters> DATABASE = new();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            Debug.Log("Initializing ScriptableObjectDataBase...");
            
            DATABASE.Clear();
            var scriptableObjects = Resources.LoadAll<SOLevelParameters>("Data");

            foreach (var scriptableObject in scriptableObjects)
            {
                DATABASE.Add(scriptableObject.name, scriptableObject);
            }
        }

        public static SOLevelParameters GetByName(string name)
        {
            if (DATABASE.TryGetValue(name, out SOLevelParameters levelParameters))
            {
                return levelParameters;
            }
            
            Debug.LogWarning($"ScriptableObject with name {name} not found in database.");
            return null;
        }
    }
}