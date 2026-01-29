using UnityEngine;

namespace Components.GameEventSystem
{
    public static class PersistantData
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            CurrentChunkMaterial = null;
        }
        
        public static Material CurrentChunkMaterial;
    }
}