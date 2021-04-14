using _Scripts.Units.Base;

namespace _Scripts.Units.Weapons
{
    public interface IBullet
    {
        void Initialize(BattleUnitBase source, WeaponData weaponData);
    }
}