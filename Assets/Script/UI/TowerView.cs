using TD.GamePlay;
using TD.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TD.UI
{
    public class TowerView : MonoBehaviour
    {
        public TowerType towerType;
        public Button _spawnButton;
        [SerializeField] private GameObject _lockedView;

        public void SetTowerView(bool isLocked)
        {
            if (_lockedView != null)
            {
                _spawnButton.interactable = isLocked;
                _lockedView.SetActive(!isLocked);
            }
        }

        public void ClickOnSpawnButton()
        {
            TowerManager.Instance.SelectTowerToBuild(towerType);
            MainUIManager.Instance.GetView<AllTowerCardView>().SetDeselectTowerView();
        }

        public void OnDeselectTowerButtonClick()
        {
            gameObject.SetActive(false);
            TowerManager.Instance.SelectTowerToBuild(towerType);
        }
    }
}