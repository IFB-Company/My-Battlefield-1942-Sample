using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Controllers
{
    public class UnitsPoolSpawner : MonoBehaviour
    {
        [SerializeField] private int _inititalValue = 10;
        private List<GameObject> _unitsPool;
        
        private void Start()
        {
            _unitsPool = new List<GameObject>(_inititalValue);
        }

        public GameObject Spawn(GameObject prefab, Vector3 posToSpawn)
        {
            var freeUnit = GetFreeUnitFromPool(prefab);
            freeUnit.transform.position = posToSpawn;
            return freeUnit;
        }

        private GameObject GetFreeUnitFromPool(GameObject prefab)
        {
            GameObject freeUnit = _unitsPool.FirstOrDefault(u => 
                !u.activeInHierarchy && u.name.Contains(prefab.name));
            if (freeUnit == null)
            {
                freeUnit = Instantiate(prefab);
                _unitsPool.Add(freeUnit);
            }
            else
            {
                freeUnit.SetActive(true);
            }

            return freeUnit;
        }
    }
}