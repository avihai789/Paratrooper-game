using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public event Action SpawnPlayer;
    
    void Start()
    {
        Fly();
    }

    private void Fly()
    {
        transform.DOMove(new Vector3(40, 30, 50), 4).onComplete += () =>
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
