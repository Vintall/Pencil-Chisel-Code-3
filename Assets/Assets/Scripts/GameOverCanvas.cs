using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameOverCanvas : MonoBehaviour
    {
        private static GameOverCanvas instance;
        public static GameOverCanvas Instance => instance;

        [SerializeField] private GameObject gameoverpanel;
        private void Awake()
        {
            instance = this;
        }

        public void OnGameOver()
        {
            gameoverpanel.SetActive(true);
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
