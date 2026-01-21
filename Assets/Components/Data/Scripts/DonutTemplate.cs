using UnityEngine;

namespace Components.Data
{
    [CreateAssetMenu(menuName = "Data/DonutTemplate")]
    public class DonutTemplate : ScriptableObject
    {
        [SerializeField] private GameObject _basePrefab;
        [SerializeField] private GameObject _toppingPrefab;
        [SerializeField] private GameObject _icingPrefab;
        
        public GameObject BasePrefab => _basePrefab;
        public GameObject ToppingPrefab => _toppingPrefab;
        public GameObject IcingPrefab => _icingPrefab;
    }

    public class DonutCreator : MonoBehaviour
    {
        private void CreateDonut(DonutTemplate template)
        {
            Instantiate(template.BasePrefab);
            Instantiate(template.ToppingPrefab);
            Instantiate(template.IcingPrefab);
        }
    }
}