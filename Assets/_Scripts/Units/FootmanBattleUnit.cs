using UnityEngine;

namespace _Scripts.Units
{
    public class FootmanBattleUnit : GroundBattleUnit
    {
        protected override void Fire()
        {
            Debug.Log($"Gun fire!");
        }
        
    }
}
