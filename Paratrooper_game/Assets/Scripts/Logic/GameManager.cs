using Paratrooper.Config;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    [SerializeField] private Presenter _presenter;

    private TimerLogic _timer;
    private CoinsLogic _coinsLogic;

    private int _currentLevel;


    public const int MAX_LEVEL = 2;

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
        LevelEnd(false);
    }

    private void InitCoins(Config.LevelData currentLevelData)
    {
        _coinsLogic = new CoinsLogic();
        _coinsLogic.UpdateCoinsCollectedAmount += (coinsCollected, coinsToCollect) =>
            _presenter.SetCoinsText(coinsCollected, coinsToCollect);
        _coinsLogic.AllCoinsCollected += LevelEnd;
        _coinsLogic.InitCoins(currentLevelData);
        SpawnCoins(currentLevelData.coinsToSpawn);
    }
    
    private void SpawnCoins(int coinsToSpawn)
    {
        _presenter.CoinSpawned += CoinSpawned;
        _presenter.SpawnCoins(coinsToSpawn);
        _presenter.CoinSpawned -= CoinSpawned;
        
    }

    private void CoinSpawned(Coin coin)
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
            _timer.TimerEnd -= TimerEnd;
            _timer.TimerChanged -= (time) => _presenter?.SetTimerText(time);
            _settings.isLevelWon = false;
            SceneManager.LoadScene("LevelEnd", LoadSceneMode.Single);
        }
    }
}