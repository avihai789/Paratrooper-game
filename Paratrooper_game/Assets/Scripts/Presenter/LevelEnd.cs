using Paratrooper.Presenter;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private LevelWin levelWin;
    [SerializeField] private LevelFail levelFail;
    [SerializeField] private Settings settings;
    
    
    public void Start()
    {
        if (settings.isLevelWon)
        {
            levelWin.gameObject.SetActive(true);
            levelWin.SetData(settings.currentLevel, settings.Config.levelsData.Length);
        }
        else
        {
            levelFail.gameObject.SetActive(true);
        }
    }
}
