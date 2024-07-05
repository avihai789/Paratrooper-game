using System;
using System.Collections.Generic;
using Cinemachine;
using Paratrooper.Config;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Presenter : MonoBehaviour
{
    [SerializeField] private Plane _plane;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform coinsParent;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Transform _playerCameraRoot;
    [SerializeField] private CinemachineVirtualCamera _playerCamera;

    private List<Coin> _coinsList;

    public event Action<Coin> CoinSpawned;

    private void Start()
    {
        _coinsList = new List<Coin>();
        _plane.SpawnPlayer += SpawnPlayer;
    }

    private void SpawnPlayer()
    {
        _plane.SpawnPlayer -= SpawnPlayer;
        _player.SetActive(true);
        _player.transform.position = _plane.transform.position;
        _playerCamera.Follow = _playerCameraRoot;
    }

    public void SpawnCoins(int coinsToSpawn)
    {
        for (var i = 0; i < coinsToSpawn; i++)
        {
            SpawnCoin();
        }
    }

    public void SetCoinsText(int coinsCollected, int coinstoCollect)
    {
        _coinsText.text = $"{coinsCollected}/{coinstoCollect}";
    }
    
    public void SetTimerText(float time)
    {
        _timerText.text = time.ToString("F2");
    }

    public void SpawnCoin()
    {
        float x = Random.Range(0, _terrain.terrainData.size.x);
        float z = Random.Range(0, _terrain.terrainData.size.z);

        float y = _terrain.SampleHeight(new Vector3(x, 0, z));

        y += 1.0f;

        Vector3 spawnPosition = new Vector3(x, y, z);
        var coin = Instantiate(_coinPrefab, spawnPosition, Quaternion.identity, coinsParent);
        CoinSpawned?.Invoke(coin);

        _coinsList.Add(coin);
    }
}