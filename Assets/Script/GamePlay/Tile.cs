using TD.Manager;
using TD.TowerHandler;
using TD.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TD.GamePlay
{
    public class Tile : MonoBehaviour
    {
        private BaseTower _baseTower;
        public bool canWePlaceTowerHere;

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!canWePlaceTowerHere)
            {
                MainUIManager.Instance.GetView<ToastMassageView>().ShowView("You can't place a tower here", true);
                return;
            }

            if (_baseTower != null)
            {
                TowerManager.Instance.SelectTile(this);
                return;
            }

            if (!TowerManager.Instance.IsAnyTowerSelected)
                return;

            BaseTower baseTower = TowerManager.Instance.SpawnNewTowerOnTile(transform);
            if (baseTower != null)
                _baseTower = baseTower;
        }
    }
}