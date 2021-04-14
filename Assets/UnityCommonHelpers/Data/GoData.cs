using UnityEngine;

namespace Common.Data
{
    [System.Serializable]
    public class GoData
    {
        [SerializeField] private GameObject[] _gameObjects;
        [SerializeField] private bool _isInverseActivation;

        public void SetActiveObjects(bool isActive)
        {
            for (int i = 0; i < _gameObjects.Length; i++)
            {
                GameObject go = _gameObjects[i];
                if (go != null)
                {
                    if (_isInverseActivation)
                    {
                        go.SetActive(!isActive); 
                    }
                    else
                    {
                        go.SetActive(isActive); 
                    }
                }
            }
        }
    }  
}

