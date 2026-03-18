using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TD.EnemyMovement;
using TD.LevelData;
using TD.Manager;
using TD.UI;
using UnityEngine;

namespace TD.EnemyHandler
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public static EnemyWaveManager Instance;

        [SerializeField] private float _timeBetweenWaves = 2f;

        private float _countdown = 2f;
        private Transform spawnPoint;
        private int waveIndex = 0;
        private LevelDataSO _currentLevelDataSO;
        private List<Enemy> _enemies = new();
        [ShowInInspector] private Dictionary<int, Dictionary<EnemyType, int>> _enemiesToSpawnDic = new();

        private int _enemiesAlive;
        private bool _isSpawningWave;
        private Coroutine _spawnLoopRoutine;

        private void Awake()
        {
            Instance = this;
        }

        public void OnEnable()
        {
            GameManager.onLevelFailed += StopWaveCoroutine;
            GameManager.onLevelExit += ClearEnemiesToSpawnDic;
        }

        public void OnDisable()
        {
            GameManager.onLevelFailed -= StopWaveCoroutine;
            GameManager.onLevelExit -= ClearEnemiesToSpawnDic;
        }

        private void ClearEnemiesToSpawnDic()
        {
            StopWaveCoroutine();
            ResetWaveManager();
            _enemiesToSpawnDic.Clear();
        }

        public void SetupEnemyWaveManager()
        {
            spawnPoint = CheckpointsManager.Instance.defaultCheckpoint.CheckpointTransform;
            _currentLevelDataSO = GameManager.Instance.levelDataSOs[GameManager.Instance.GetCurrentLevel()];
            _enemiesToSpawnDic = _currentLevelDataSO.GetEnemiesToSpawn();
            StartSpawnWave();
        }

        private void StartSpawnWave()
        {
            StopWaveCoroutine();
            _spawnLoopRoutine = StartCoroutine(SpawnLoop());
        }

        private void StopWaveCoroutine()
        {
            ResetWaveManager();
            if (_spawnLoopRoutine != null)
                StopCoroutine(_spawnLoopRoutine);
        }

        private void ResetWaveManager()
        {
            _isSpawningWave = false;
            _enemiesAlive = 0;
            waveIndex = 0;
        }

        private IEnumerator SpawnLoop()
        {
            while (_currentLevelDataSO != null)
            {
                if (IsLevelComplete())
                {
                    yield return new WaitForSeconds(1f);
                    GameManager.Instance.LevelComplete();
                    yield break;
                }

                if (_isSpawningWave || _enemiesAlive > 0)
                {
                    yield return null;
                    continue;
                }

                _countdown -= Time.deltaTime;
                _countdown = Mathf.Clamp(_countdown, 0f, Mathf.Infinity);
                MainUIManager.Instance.GetView<GamePlayView>().SetNextWaveText(string.Format("{0:0.0}", _countdown));

                if (_countdown <= 0f && !IsLevelComplete())
                {
                    MainUIManager.Instance.GetView<GamePlayView>().HideNextWaveText();
                    StartCoroutine(SpawnWave());
                    _countdown = _timeBetweenWaves;
                }

                yield return null;
            }
        }

        public void OnEnemyDie()
        {
            _enemiesAlive = Mathf.Max(0, _enemiesAlive - 1);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
        }

        public Enemy[] GetEnemies()
        {
            return _enemies.ToArray();
        }

        private void SpawnEnemy(Enemy enemy)
        {
            Enemy newEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation, transform);
            newEnemy.SetHealthMultiplier(_currentLevelDataSO.GetHealthMultiplier(waveIndex));
            EnemyMovementHandler.Instance.AddEnemy(newEnemy);
            _enemies.Add(newEnemy);
        }

        private bool IsLevelComplete()
        {
            return _enemiesAlive <= 0 && waveIndex >= _currentLevelDataSO.enemyWaveData.Count;
        }

        private EnemyType GetAvailableEnemyType()
        {
            List<EnemyType> availableEnemyTypes = _enemiesToSpawnDic[waveIndex].Keys.ToList();
            foreach (var enemy in _enemiesToSpawnDic[waveIndex])
            {
                if (enemy.Value <= 0)
                    availableEnemyTypes.Remove(enemy.Key);
            }
            return availableEnemyTypes[Random.Range(0, availableEnemyTypes.Count)];
        }

        private IEnumerator SpawnWave()
        {
            PlayerInfoManager.Instance.Rounds++;
            _isSpawningWave = true;
            _enemiesAlive = _currentLevelDataSO.GetTotalEnemiesInWave(waveIndex);

            for (int i = 0; i < _currentLevelDataSO.GetTotalEnemiesInWave(waveIndex); i++)
            {
                EnemyType enemyType = GetAvailableEnemyType();
                SpawnEnemy(EnemyManager.Instance.GetEnemy(enemyType));
                _enemiesToSpawnDic[waveIndex][enemyType]--;
                float delay = Random.Range(0.5f, 1f);
                yield return new WaitForSeconds(delay);
            }

            waveIndex++;
            _isSpawningWave = false;
        }
    }
}