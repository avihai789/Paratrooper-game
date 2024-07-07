using System;
using Paratrooper.Config;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Settings _settings;

    private TimerLogic _timer;
    private CoinsLogic _coinsLogic;

    private int _currentLevel;

    private const int ANIMATION_TIME = 6;

    public static GameManager Instance;
    
    public event Action SpawnCoin;
    public event Action<float> TimeChanged;
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
        _currentLevel = _settings.currentLevel;
        CheckLevelData();
        StartLevel(_settings.Config.levelsData[_currentLevel - 1]);
    }

    private void Update()
    {
        _timer?.Update(Time.deltaTime);
    }

    private void StartLevel(Config.LevelData currentLevelData)
    {
        InitCoins(currentLevelData);
        InitTimer(currentLevelData.time + ANIMATION_TIME);
    }

    private void CheckLevelData()
    {
        if (_settings.currentLevel > _settings.Config.levelsData.Length)
        {
            _settings.currentLevel = 1;
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

    private void InitCoins(Config.LevelData currentLevelData)
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
        _coinsLogic.CollectCoin(coin);
    }
    
    private void LevelEnd(bool isLevelWon)
    {
        _timer.StopTimer();
        if (isLevelWon)
        {
            _settings.isLevelWon = true;
            _settings.currentLevel++;
            SceneManager.LoadSceneAsync("LevelEnd", LoadSceneMode.Single);
        }
        else
        {
            _settings.isLevelWon = false;
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