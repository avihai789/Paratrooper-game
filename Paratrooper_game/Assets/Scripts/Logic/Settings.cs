using Paratrooper.Config;
using Paratrooper.Presenter;
using UnityEngine;

// This scriptable object is used to store the settings of the game
[CreateAssetMenu]
public class Settings : ScriptableObject
{
    public Config Config;
    
    public int currentLevel = 1;
    
    public bool isLevelWon = false;

    public ViewSwitcher.ViewState viewState;
}
