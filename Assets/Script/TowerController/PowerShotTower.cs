namespace TD.TowerHandler
{
    public class PowerShotTower : BaseTower
    {
        public float damageMultiplier = 25f;

        protected override float GetDamage()
        {
            float newDamage = _towerInfoDataSO.damage * damageMultiplier / 100f;
            return _towerInfoDataSO.damage + newDamage;
        }

    }
}