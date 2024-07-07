using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Paratrooper.Presenter
{
    // This class is used to control the level win prefab
    public class LevelWin : MonoBehaviour, ILevelEnd
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI buttonText;

        private bool _gameEnded = false;

        public void OnClick()
        {
            if (_gameEnded)
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
        }

        public void SetData(int currentLevel, int maxLevel)
        {
            if (currentLevel > maxLevel)
            {
                _gameEnded = true;
                buttonText.text = "Exit Game";
                levelText.text = "Game Completed!";
            }
        }
    }
}