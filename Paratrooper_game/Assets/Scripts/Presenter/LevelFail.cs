using UnityEngine;
using UnityEngine.SceneManagement;


namespace Paratrooper.Presenter
{
    // This class is used to control the level fail prefab
    public class LevelFail : MonoBehaviour, ILevelEnd
    {
        public void OnClick()
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
    }
}