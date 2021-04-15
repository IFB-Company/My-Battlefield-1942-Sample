using _Scripts.Player.Controllers.Base;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units.Vehicle
{
    public class UnitVehiclePilotController : MonoBehaviour
    {
        [SerializeField] private Camera _localCamera;
        
        private IPlayerController _playerController;

        private BattleVehicleBase _vehicle;
        
        private void Awake()
        {
            _playerController = GetComponentInParent<IPlayerController>();
            Assert.IsNotNull(_playerController, "_playerController != null");
            
        }

        [ContextMenu("Enter nearest vehicle")]
        private void EnterNearestVehicle()
        {
            var results = Physics.OverlapSphere(transform.position, 2f);
            foreach (var res in results)
            {
                if (res.TryGetComponent(out BattleVehicleBase battleVehicle))
                {
                    EnterInVehicle(battleVehicle);
                }
            }
        }

        [ContextMenu("Exit current vehicle")]
        private void ExitVehicle()
        {
            LeaveFromVehicle();
        }
        
        public void EnterInVehicle(BattleVehicleBase battleVehicle)
        {
            _vehicle = battleVehicle;
            if (_localCamera != null && battleVehicle.CameraPosition !=null)
            {
                var vehicleCamPos = battleVehicle.CameraPosition;
                _localCamera.transform.position = vehicleCamPos.position;
                _localCamera.transform.rotation = vehicleCamPos.rotation;
                _localCamera.transform.parent = vehicleCamPos;
            }

            var footman = _playerController.FootmanBattleUnit;
            footman.gameObject.SetActive(false);
            _playerController.SetCurrentBattleUnit(_vehicle);
            _vehicle.EnterVehicle(footman);
        }

        public void LeaveFromVehicle()
        {
            if (_vehicle != null)
            {
                var footman = _playerController.FootmanBattleUnit;
                if (_localCamera != null && footman != null)
                {
                    var footmanCamPos = footman.CameraPosition;
                    _localCamera.transform.position = footmanCamPos.position;
                    _localCamera.transform.rotation = footmanCamPos.rotation;
                    _localCamera.transform.parent = footmanCamPos;
                } 
                footman.gameObject.SetActive(true);
                if (_vehicle.LeavePosition != null)
                {
                    footman.transform.position = _vehicle.LeavePosition.position;
                }
                _playerController.SetCurrentBattleUnit(footman);
                _vehicle.LeaveVehicle();
            }
            
        }
    }
}
