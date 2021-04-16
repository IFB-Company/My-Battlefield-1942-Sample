using System;
using UnityEngine;

namespace _Scripts.Units.Vehicle
{
    public class UnitVehicleDetector : MonoBehaviour
    {
        [SerializeField] private float _findRange = 1f;
        [Range(1, 100)]
        [SerializeField] private int _cacheSize = 5;
        [SerializeField] private bool _isActive;
        public bool IsActive => _isActive;


        private Collider[] _cache;
        
        public event Action<BattleVehicleBase> OnVehicleDetected;
        public event Action OnNoVehiclesNearly;

        private BattleVehicleBase _lastDetectedVehicle;
        
        public void SetIsActive(bool isActive)
        {
            this._isActive = isActive;
        }

        private void Awake()
        {
            _cache = new Collider[_cacheSize];
        }

        private void Update()
        {
            if (!_isActive)
                return;

            FindVehicleLoop();
            
#if UNITY_STANDALONE || UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.E))
            {
                var pilotController = GetComponent<UnitVehiclePilotController>();
                if (pilotController != null)
                {
                    if (pilotController.IsOnVehicle)
                    {
                        pilotController.LeaveFromVehicle();
                    }
                    else
                    {
                        if (_lastDetectedVehicle)
                        {
                            pilotController.EnterInVehicle(_lastDetectedVehicle);   
                        }
                    }
                }
            }
#endif
        }

        private void FindVehicleLoop()
        {
            int foundSize = Physics.OverlapSphereNonAlloc(transform.position, _findRange, _cache);
            if (foundSize > 0)
            {
                bool isVehicleFound = false;
                foreach (var col in _cache)
                {
                    if(col == null)
                        continue;
                    
                    if (col.TryGetComponent(out BattleVehicleBase battleVehicle))
                    {
                        isVehicleFound = true;
                        _lastDetectedVehicle = battleVehicle;
                        OnVehicleDetected?.Invoke(_lastDetectedVehicle);
                    }
                }

                if (!isVehicleFound)
                {
                    OnNoVehiclesNearly?.Invoke();
                }
            }
        }
    }
}
