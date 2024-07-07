using System.IO;
using UnityEngine;

namespace Paratrooper.Config
{
    public class ConfigLoader
    {
        private Config _loadedConfig;
        
        public Config LoadConfig()
        {
            const string path = "Assets/Scripts/Config/config.json";
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                _loadedConfig = JsonUtility.FromJson<Config>(json);
            }
            else
            {
                Debug.LogError("Error loading data");
            }
            return _loadedConfig;
        }
    }
}