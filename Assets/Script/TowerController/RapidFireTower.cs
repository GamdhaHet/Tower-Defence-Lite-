namespace TD.TowerHandler
{
    public class RapidFireTower : BaseTower
    {
        public float fireRateMultiplier = 25f;

        protected override float GetFireRate()
        {
            float newFireRate = _towerInfoDataSO.fireRate * fireRateMultiplier / 100f;
            return _towerInfoDataSO.fireRate + newFireRate;
        }
    }
}