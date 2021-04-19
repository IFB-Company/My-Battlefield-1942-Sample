using _Scripts.Player.Controllers;
using _Scripts.Units;
using _Scripts.Units.Base;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UnitHpBar : MonoBehaviour
    {
        [SerializeField] private LocalPlayerController _localPlayerController;
        [SerializeField] private Gradient _hpGradient;
        [SerializeField] private Image _hpBarImg;
        [SerializeField] private Transform _hpBarLayout;

        private HittableObject _lastHittable;
        
        private void Awake()
        {
            Assert.IsNotNull(_localPlayerController, "_localPlayerController != null");
            Assert.IsNotNull(_hpGradient, "_hpGradient != null");
            Assert.IsNotNull(_hpBarImg, "_hpBarImg != null");
            Assert.IsNotNull(_hpBarLayout, "_hpBarLayout != null");
            
            _localPlayerController.OnCurrentUnitChanged += LocalPlayerControllerOnCurrentUnitChanged;
        }

        private void OnDestroy()
        {
            _localPlayerController.OnCurrentUnitChanged -= LocalPlayerControllerOnCurrentUnitChanged;
            
            if (_lastHittable != null)
            {
                _lastHittable.OnDamaged -= LastHittableOnDamaged;
                _lastHittable.OnDie -= LastHittableOnDie;
            }
        }

        private void LocalPlayerControllerOnCurrentUnitChanged(BattleUnitBase unit)
        {
            UpdateHpBarByUnit(unit);
        }

        private void UpdateHpBarByUnit(BattleUnitBase newUnit)
        {
            _hpBarLayout.gameObject.SetActive(true);
            
            var unitHittable = newUnit.GetComponentInChildren<HittableObject>();
            if (unitHittable == null)
            {
                Debug.LogError($"{nameof(HittableObject)} is missing on {nameof(BattleUnitBase)}- {newUnit.name}!");
                return;
            }

            if (_lastHittable != null)
            {
                _lastHittable.OnDamaged -= LastHittableOnDamaged;
                _lastHittable.OnDie -= LastHittableOnDie;
            }

            unitHittable.OnDamaged += LastHittableOnDamaged;
            unitHittable.OnDie += LastHittableOnDie;
            
            _lastHittable = unitHittable;

            UpdateHpByCurrentUnit();
        }

        private void LastHittableOnDie()
        {
            _hpBarLayout.gameObject.SetActive(false);
        }

        private void LastHittableOnDamaged(int dmg)
        {
            UpdateHpByCurrentUnit();
        }

        private void UpdateHpByCurrentUnit()
        {
            var currentHp = _lastHittable.CurrentHittableData.CurrentHp;
            var maxHp = _lastHittable.UnitProperties.MaxHp;
            var hpNormalized = (float) currentHp / (float) maxHp;

            _hpBarImg.fillAmount = hpNormalized;
            _hpBarImg.color = _hpGradient.Evaluate(hpNormalized);
        }
    }
}
