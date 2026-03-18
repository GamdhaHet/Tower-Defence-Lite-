using System.Collections.Generic;
using TD.EnemyHandler;
using TD.LevelData;
using TD.UI;
using UnityEngine;

namespace TD.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public List<LevelDataSO> levelDataSOs;
        public GameObject gamePlayView;
        public PlayerBase playerBase;
        public int playerLives;
        public int currentLevel;

        private void Awake()
        {
            Instance = this;
        }

        public void StartGame()
        {
            LoadData();
            gamePlayView.SetActive(true);
            MainUIManager.Instance.LoadGamePlayView();
            EnemyWaveManager.Instance.SetupEnemyWaveManager();
            int currentLevel = GetCurrentLevel() + 1;
            MainUIManager.Instance.GetView<GamePlayView>().SetCurrentLevelText(currentLevel.ToString());
        }

        public void LevelFailed()
        {
            RaiseOnLevelFailed();
            MainUIManager.Instance.ShowView<LevelFailView>();
        }

        public void LevelComplete()
        {
            currentLevel++;
            SaveData();
            RaiseOnLevelComplete();
            GiveRewards();
            MainUIManager.Instance.GetView<LevelCompleteView>().SetReward(levelDataSOs[GetCurrentLevel()].levelWinRewards);
        }

        private void GiveRewards()
        {
            foreach (var reward in levelDataSOs[GetCurrentLevel()].levelWinRewards)
            {
                CurrencyDataManager.Instance.AddReward(reward);
            }
        }

        public void OnEnemyHitPlayerBase()
        {
            playerLives--;
            MainUIManager.Instance.GetView<TopBarView>().SetHeartView(playerLives);
            if (playerLives <= 0)
                LevelFailed();
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        }

        public void LoadData()
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        }

        public void PauseGameTimeScale()
        {
            Time.timeScale = 0f;
        }

        public void ResumeGameTimeScale()
        {
            Time.timeScale = 1f;
        }

        public int GetCurrentLevel()
        {
            if (currentLevel >= levelDataSOs.Count)
                return levelDataSOs.Count - 1;
            return currentLevel;
        }

        public void OnGameLevelExit()
        {
            gamePlayView.SetActive(false);
            RaiseOnLevelExit();
        }

        public delegate void OnLevelComplete();
        public static event OnLevelComplete onLevelComplete;
        public static void RaiseOnLevelComplete()
        {
            onLevelComplete?.Invoke();
        }

        public delegate void OnLevelFailed();
        public static event OnLevelFailed onLevelFailed;
        public static void RaiseOnLevelFailed()
        {
            onLevelFailed?.Invoke();
        }

        public delegate void OnLevelExit();
        public static event OnLevelExit onLevelExit;
        public static void RaiseOnLevelExit()
        {
            onLevelExit?.Invoke();
        }
    }
}