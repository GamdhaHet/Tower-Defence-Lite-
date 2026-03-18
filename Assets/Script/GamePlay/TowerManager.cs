using System.Collections.Generic;
using TD.Manager;
using TD.TowerHandler;
using TD.UI;
using UnityEngine;

namespace TD.GamePlay
{
    public class TowerManager : MonoBehaviour
    {
        public static TowerManager Instance;

        [SerializeField] private List<BaseTower> _allTowerInfoDataSOs;

        [SerializeField] private TowerType _currentlySelectedTowerType;
        private List<BaseTower> _towers = new();
        private Tile selectedTile;
        private const string DualShotTower = "DualShotTowerPurchase";
        private bool _isDualShotTowerPurchased;

        public bool IsDualShotTowerPurchased { get { return _isDualShotTowerPurchased; } }
        public bool IsAnyTowerSelected { get { return _currentlySelectedTowerType != TowerType.None; } }
        public bool IsEnoughCurrency { get { return CurrencyDataManager.Instance.IsEnoughCurrency(GetTower(_currentlySelectedTowerType).towerInfoDataSO.purchaseCost); } }

        private void Awake()
        {
            Instance = this;
            LoadData();
        }

        public void OnEnable()
        {
            GameManager.onLevelExit += ClearAllTowers;
        }

        public void OnDisable()
        {
            GameManager.onLevelExit -= ClearAllTowers;
        }

        private void ClearAllTowers()
        {
            _currentlySelectedTowerType = TowerType.None;
            selectedTile = null;
            MainUIManager.Instance.GetView<AllTowerCardView>().OnGameRestart();
            foreach (var tower in _towers)
            {
                Destroy(tower.gameObject);
            }
            _towers.Clear();
        }

        public void SelectTile(Tile tile)
        {
            if (selectedTile == tile)
            {
                DeselectTower();
                return;
            }

            selectedTile = tile;
            _currentlySelectedTowerType = TowerType.None;
        }

        public void DeselectTower()
        {
            selectedTile = null;
        }

        public void SelectTowerToBuild(TowerType towerType)
        {
            _currentlySelectedTowerType = towerType;
            DeselectTower();
        }

        public void OnDualShotTowerPurchased()
        {
            _isDualShotTowerPurchased = true;
            SaveData();
        }

        public void LoadData()
        {
            _isDualShotTowerPurchased = PlayerPrefs.GetInt(DualShotTower, 0) == 1;
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt(DualShotTower, _isDualShotTowerPurchased ? 1 : 0);
            PlayerPrefs.Save();
        }


        public BaseTower GetTower(TowerType towerType)
        {
            return _allTowerInfoDataSOs.Find(x => x.towerInfoDataSO.towerType == towerType);
        }

        public BaseTower SpawnNewTowerOnTile(Transform tileTransform)
        {
            if (!IsEnoughCurrency)
            {
                MainUIManager.Instance.GetView<AllTowerCardView>().SetDeselectTowerView();
                MainUIManager.Instance.GetView<ToastMassageView>().ShowView("Not enough money to build that!", true);
                return null;
            }

            CurrencyDataManager.Instance.Subtract(GetTower(_currentlySelectedTowerType).towerInfoDataSO.purchaseCost);

            BaseTower tower = Instantiate(GetTower(_currentlySelectedTowerType), tileTransform.position, Quaternion.identity);
            tower.transform.localPosition = new Vector3(tileTransform.position.x, 0, tileTransform.position.z);
            _towers.Add(tower);
            return tower;
        }
    }
}