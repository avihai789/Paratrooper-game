using System;
using Paratrooper.Config;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    [SerializeField] private Presenter _presenter;
    
    private TimerLogic _timer;
    
    private int _currentLevel;
    private int _coinsToCollect;
    private int _coinsCollected;
    
    public static GameManager Instance;
    

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
        StartLevel(_settings.Config.levelsData[_currentLevel - 1]);
    }
    
    private void Update()
    {
        _timer.Update(Time.deltaTime);
    }
    
    private void StartLevel(Config.LevelData currentLevelData)
    {
        InitCoins(currentLevelData);
        InitTimer(currentLevelData.time);
    }
    private void InitTimer(int time)
    {
        _timer = new TimerLogic(time);
        _timer.TimerEnd += TimerEnd;
        _timer.TimerChanged += (time) => _presenter.SetTimerText(time);
    }

    private void TimerEnd()
    {
        _timer.StopTimer();
        _timer.TimerChanged -= (time) => _presenter?.SetTimerText(time);
        Debug.Log("Level failed");
    }

    private void InitCoins(Config.LevelData currentLevelData)
    {
        _coinsToCollect = currentLevelData.coinsToCollect;
        _coinsCollected = 0;
        SpawnCoins(currentLevelData);
    }
    
    private void SpawnCoins(Config.LevelData currentLevelData)
    {
        _presenter.CoinSpawned += CoinSpawned;
        _presenter.SpawnCoins(currentLevelData);
        _presenter.SetCoinsText(_coinsCollected, _coinsToCollect);
    }

    private void CoinSpawned(Coin coin)
    {
        coin.CoinCollected += CoinCollected;
    }

    private void CoinCollected(Coin coin)
    {
        Destroy(coin.gameObject);
        _coinsCollected++;
        _presenter.SetCoinsText(_coinsCollected, _coinsToCollect);
        
        if (_coinsCollected == _coinsToCollect)
        {
            Debug.Log("Level completed");
        }
    }
}
