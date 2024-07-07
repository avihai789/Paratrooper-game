using System;
using Paratrooper.Presenter;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Paratrooper.Logic
{
    // This class is used to control the game logic
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Settings settings;

        private TimerLogic _timer;
        private CoinsLogic _coinsLogic;

        private int _currentLevel;

        private const int ANIMATION_TIME = 6;

        public static GameManager Instance;

        public event Action SpawnCoin;
        public event Action<float> TimeChanged;
        
        public event Action<string> LevelStarted;
        public event Action<int, int> UpdateCoinsCollectedAmount;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void Start()
        {
            _currentLevel = settings.currentLevel;
            CheckLevelData();
            StartLevel(settings.Config.levelsData[_currentLevel - 1]);
        }

        private void Update()
        {
            _timer?.Update(Time.deltaTime);
        }

        private void StartLevel(Config.Config.LevelData currentLevelData)
        {
            InitCoins(currentLevelData);
            InitTimer(currentLevelData.time + ANIMATION_TIME);
            LevelStarted?.Invoke(currentLevelData.levelName);
        }

        private void CheckLevelData()
        {
            if (settings.currentLevel > settings.Config.levelsData.Length)
            {
                settings.currentLevel = 1;
                _currentLevel = 1;
            }
        }

        private void InitTimer(int time)
        {
            _timer = new TimerLogic(time);
            _timer.TimerEnd += TimerEnd;
            _timer.TimerChanged += (time) => TimeChanged?.Invoke(time);
        }

        private void TimerEnd()
        {
            LevelEnd(false);
        }

        private void InitCoins(Config.Config.LevelData currentLevelData)
        {
            _coinsLogic = new CoinsLogic();
            _coinsLogic.UpdateCoinsCollectedAmount += (coinsCollected, coinsToCollect) =>
                UpdateCoinsCollectedAmount?.Invoke(coinsCollected, coinsToCollect);
            _coinsLogic.AllCoinsCollected += LevelEnd;
            _coinsLogic.InitCoins(currentLevelData);
            SpawnCoins(currentLevelData.coinsToSpawn);
        }

        private void SpawnCoins(int coinsToSpawn)
        {
            for (var i = 0; i < coinsToSpawn; i++)
            {
                SpawnCoin?.Invoke();
            }
        }

        public void CoinSpawned(Coin coin)
        {
            coin.CoinCollected += CoinCollected;
        }

        private void CoinCollected(Coin coin)
        {
            _coinsLogic.CollectCoin();
            Destroy(coin.gameObject);
        }

        private void LevelEnd(bool isLevelWon)
        {
            _timer.StopTimer();
            if (isLevelWon)
            {
                settings.isLevelWon = true;
                settings.currentLevel++;
                SceneManager.LoadSceneAsync("LevelEnd", LoadSceneMode.Single);
            }
            else
            {
                settings.isLevelWon = false;
                SceneManager.LoadScene("LevelEnd", LoadSceneMode.Single);
            }
        }

        private void OnDestroy()
        {
            _timer.TimerEnd -= TimerEnd;
            _timer.TimerChanged -= (time) => TimeChanged?.Invoke(time);

            _coinsLogic.AllCoinsCollected -= LevelEnd;
            _coinsLogic.UpdateCoinsCollectedAmount -= (coinsCollected, coinsToCollect) =>
                UpdateCoinsCollectedAmount?.Invoke(coinsCollected, coinsToCollect);
        }
    }
}