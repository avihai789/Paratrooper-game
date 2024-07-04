using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFail  : MonoBehaviour, ILevelEnd
{
    public void OnClick()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
