using Paratrooper.Config;
using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    public Config Config;
    
    public int currentLevel = 1;
    
    public bool isLevelWon = false;
}
