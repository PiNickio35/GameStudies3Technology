using UnityEngine;

namespace _PROJECT.Scripts
{
    public enum GameState { Explore, Paused }

    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject pausePanel;

        public GameState state = GameState.Explore;
        public static GameController Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Pause()
        {
            state = GameState.Paused;
            Debug.Log("Pause");
            pausePanel.SetActive(true);
        }

        public void UnPause()
        {
            state = GameState.Explore;
            pausePanel.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}