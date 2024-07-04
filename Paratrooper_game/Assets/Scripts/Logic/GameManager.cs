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
    
    private int _currentLevel;
    private int coinsToCollect;
    private int coinsCollected;
    
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
    
    private void StartLevel(Config.LevelData _currentLevelData)
    {
        InitCoins(_currentLevelData);
    }
    
    private void InitCoins(Config.LevelData _currentLevelData)
    {
        coinsToCollect = _currentLevelData.coinsToCollect;
        coinsCollected = 0;
        SpawnCoins(_currentLevelData);
    }
    
    private void SpawnCoins(Config.LevelData _currentLevelData)
    {
        _presenter.CoinSpawned += CoinSpawned;
        _presenter.SpawnCoins(_currentLevelData);
        _presenter.SetCoinsText(coinsCollected, coinsToCollect);
    }

    private void CoinSpawned(Coin coin)
    {
        coin.CoinCollected += CoinCollected;
    }

    private void CoinCollected(Coin coin)
    {
        Destroy(coin.gameObject);
        coinsCollected++;
        _presenter.SetCoinsText(coinsCollected, coinsToCollect);
        
        if (coinsCollected == coinsToCollect)
        {
            Debug.Log("Level completed");
        }
    }
}
