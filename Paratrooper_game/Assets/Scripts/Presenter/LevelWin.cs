using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin  : MonoBehaviour, ILevelEnd
{
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private TextMeshProUGUI levelText;
    public void OnClick()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void SetData(int currentLevel, int maxLevel)
    {
        if (currentLevel > maxLevel)
        {
            nextLevelButton.SetActive(false);
            levelText.text = "Game Completed!";
        } 
    }
}
