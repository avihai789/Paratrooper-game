using System;
using Paratrooper.Config;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    
    private int _currentLevel;
    
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
        Debug.Log($"Level {_currentLevelData.levelName} started");
    }

}
