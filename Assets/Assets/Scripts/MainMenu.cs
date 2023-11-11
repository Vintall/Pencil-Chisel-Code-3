using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            playButton.onClick.AddListener(OnPlayButtonClick);
            exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnPlayButtonClick()
        {
            Debug.Log($"OnPlayButtonClick");
            SceneManager.LoadScene(1);
        }

        private void OnExitButtonClick()
        {
            Debug.Log($"OnExitButtonClick");
            Application.Quit();
        }
    }
}
