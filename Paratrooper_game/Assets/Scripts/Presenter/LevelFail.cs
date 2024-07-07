using UnityEngine;
using UnityEngine.SceneManagement;


namespace Paratrooper.Presenter
{
    public class LevelFail : MonoBehaviour, ILevelEnd
    {
        public void OnClick()
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
    }
}