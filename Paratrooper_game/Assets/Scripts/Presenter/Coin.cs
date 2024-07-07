using System;
using DG.Tweening;
using UnityEngine;

namespace Paratrooper.Presenter
{
    // This class is used to control the coin view
    public class Coin : MonoBehaviour
    {
        [SerializeField] private float spinSpeed;
        
        private Tween _tween;

        public event Action<Coin> CoinCollected;

        private void Start()
        {
            StartSpinning();
        }

        private void StartSpinning()
        {
            _tween = transform.DORotate(new Vector3(360, 360, 360), 1 / (spinSpeed / 360f), RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CoinCollected?.Invoke(this);
            }
        }
        
        private void OnDestroy()
        {
            _tween.Kill();
        }
    }
}