using System;
using System.Collections.Generic;
using Paratrooper.Config;
using UnityEngine;

public class CoinsLogic : MonoBehaviour
{
    private int _coinsToCollect;
    private int _coinsCollected;
    
    public event Action<int, int> UpdateCoinsCollectedAmount;
    public event Action<bool> AllCoinsCollected;
    public void InitCoins(Config.LevelData currentLevelData)
    {
        SetCoinsToCollect(currentLevelData.coinsToCollect);
        UpdateCoinsCollectedAmount?.Invoke(_coinsCollected, _coinsToCollect);
    }
    
    private void SetCoinsToCollect(int coinsToCollect)
    {
        _coinsToCollect = coinsToCollect;
    }
    
    public void CollectCoin(Coin coin)
    {
        Destroy(coin.gameObject);
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
