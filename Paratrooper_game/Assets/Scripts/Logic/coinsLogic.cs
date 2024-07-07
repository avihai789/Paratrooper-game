using System;
using Paratrooper.Logic;
using Paratrooper.Presenter;
using UnityEngine;

namespace Paratrooper.Logic
{
    // This class is used to control the coins logic only
    public class CoinsLogic
    {
        private int _coinsToCollect;
        private int _coinsCollected;

        public event Action<int, int> UpdateCoinsCollectedAmount;
        public event Action<bool> AllCoinsCollected;

        public void InitCoins(Config.Config.LevelData currentLevelData)
        {
            SetCoinsToCollect(currentLevelData.coinsToCollect);
            UpdateCoinsCollectedAmount?.Invoke(_coinsCollected, _coinsToCollect);
        }

        private void SetCoinsToCollect(int coinsToCollect)
        {
            _coinsToCollect = coinsToCollect;
        }

        public void CollectCoin()
        {
            _coinsCollected++;
            UpdateCoinsCollectedAmount?.Invoke(_coinsCollected, _coinsToCollect);
            if (CheckIfAllCoinsCollected())
            {
                AllCoinsCollected?.Invoke(true);
            }
        }

        private bool CheckIfAllCoinsCollected()
        {
            return _coinsCollected == _coinsToCollect;
        }
    }
}
