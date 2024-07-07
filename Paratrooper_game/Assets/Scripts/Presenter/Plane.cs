using System;
using DG.Tweening;
using UnityEngine;

namespace Paratrooper.Presenter
{
    public class Plane : MonoBehaviour
    {
        public event Action SpawnPlayer;

        void Start()
        {
            Fly();
        }

        private void Fly()
        {
            transform.DOMove(new Vector3(40, 30, 50), 4).SetEase(Ease.Linear).onComplete += () =>
            {
                FlySecondHalf();
                SpawnPlayer?.Invoke();
            };
        }

        private void FlySecondHalf()
        {
            transform.DOMove(new Vector3(40, 30, 100), 4).onComplete += () => { Destroy(gameObject); };
        }
    }
}