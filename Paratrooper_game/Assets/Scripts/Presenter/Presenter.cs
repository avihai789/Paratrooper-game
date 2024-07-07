using System;
using System.Collections.Generic;
using Paratrooper.Logic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Paratrooper.Presenter
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] private Plane _plane;
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private Terrain _terrain;
        [SerializeField] private Transform coinsParent;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private ViewSwitcher _viewSwitcher;

        private GameManager _gameManager;

        private List<Coin> _coinsList;


        private void Start()
        {
            _gameManager = GameManager.Instance;
            _coinsList = new List<Coin>();
            _plane.SpawnPlayer += SpawnPlayer;
            _gameManager.SpawnCoin += SpawnCoin;
            _gameManager.TimeChanged += SetTimerText;
            _gameManager.UpdateCoinsCollectedAmount += SetCoinsText;
        }

        private void SpawnPlayer()
        {
            _plane.SpawnPlayer -= SpawnPlayer;
            _viewSwitcher.SpawnPlayer(_plane.transform.position);
        }

        private void SetCoinsText(int coinsCollected, int coinstoCollect)
        {
            _coinsText.text = $"{coinsCollected}/{coinstoCollect}";
        }

        private void SetTimerText(float time)
        {
            _timerText.text = time.ToString("F2");
        }

        private void SpawnCoin()
        {
            float x = Random.Range(0, _terrain.terrainData.size.x);
            float z = Random.Range(0, _terrain.terrainData.size.z);

            float y = _terrain.SampleHeight(new Vector3(x, 0, z));

            y += 1.0f;

            Vector3 spawnPosition = new Vector3(x, y, z);
            var coin = Instantiate(_coinPrefab, spawnPosition, Quaternion.identity, coinsParent);
            _gameManager.CoinSpawned(coin);

            _coinsList.Add(coin);
        }

        private void OnDestroy()
        {
            _gameManager.SpawnCoin -= SpawnCoin;
            _gameManager.TimeChanged -= SetTimerText;
            _gameManager.UpdateCoinsCollectedAmount -= SetCoinsText;
        }
    }
}