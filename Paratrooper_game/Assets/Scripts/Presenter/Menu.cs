using System;
using Paratrooper.Config;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Paratrooper.Presenter
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        private void Start()
        {
            LoadConfig();
        }

        public void StartGame()
        {
            LoadGameScene();
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        private void LoadGameScene()
        {
            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
        }

        private void LoadConfig()
        {
            var config = new ConfigLoader().LoadConfig();
            if (config != null)
            {
                _settings.Config = config;
            }
            else
            {
                throw new Exception("Config not loaded");
            }
        }
    }
}