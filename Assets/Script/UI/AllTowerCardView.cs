using System.Collections.Generic;
using TD.GamePlay;

namespace TD.UI
{
    public class AllTowerCardView : BaseView
    {
        public List<TowerView> towerViews;
        public TowerView _deselectTowerView;

        public override void ShowView()
        {
            SetTowerView(TowerType.DualShotTower, TowerManager.Instance.IsDualShotTowerPurchased);
            base.ShowView();
        }

        public void SetTowerView(TowerType towerType, bool isLocked)
        {
            foreach (var tower in towerViews)
            {
                if (tower.towerType == towerType)
                    tower.SetTowerView(isLocked);
            }
        }

        public void SetDeselectTowerView()
        {
            bool isActive = TowerManager.Instance.IsEnoughCurrency;
            _deselectTowerView.gameObject.SetActive(isActive);
        }

        public void OnGameRestart()
        {
            _deselectTowerView.gameObject.SetActive(false);
        }
    }
}
