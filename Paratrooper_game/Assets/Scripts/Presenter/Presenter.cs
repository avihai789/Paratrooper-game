using System.Collections.Generic;
using Paratrooper.Logic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Paratrooper.Presenter
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] private Plane plane;
        [SerializeField] private Coin coinPrefab;
        [SerializeField] private Terrain terrain;
        [SerializeField] private Transform coinsParent;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private ViewSwitcher viewSwitcher;

        private GameManager _gameManager;

        private List<Coin> _coinsList;


        private void Start()
        {
            _gameManager = GameManager.Instance;
            _coinsList = new List<Coin>();
            plane.SpawnPlayer += SpawnPlayer;
            _gameManager.SpawnCoin += SpawnCoin;
            _gameManager.TimeChanged += SetTimerText;
            _gameManager.UpdateCoinsCollectedAmount += SetCoinsText;
            _gameManager.LevelStarted += SetLevelText;
        }

        private void SpawnPlayer()
        {
            plane.SpawnPlayer -= SpawnPlayer;
            viewSwitcher.SpawnPlayer(plane.transform.position);
        }

        private void SetCoinsText(int coinsCollected, int coinstoCollect)
        {
            coinsText.text = $"{coinsCollected}/{coinstoCollect}";
        }

        private void SetTimerText(float time)
        {
            timerText.text = time.ToString("F2");
        }
        
        public void SetLevelText(string currentLevel)
        {
            levelText.text = currentLevel;
        }

        private void SpawnCoin()
        {
            float x = Random.Range(0, terrain.terrainData.size.x);
            float z = Random.Range(0, terrain.terrainData.size.z);

            float y = terrain.SampleHeight(new Vector3(x, 0, z));

            y += 1.0f;

            Vector3 spawnPosition = new Vector3(x, y, z);
            var coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, coinsParent);
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