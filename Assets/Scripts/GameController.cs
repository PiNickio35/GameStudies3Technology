using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Explore, Paused }

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winUI;

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

    private void Update()
    {
        if (PlayerController.Instance.meatCount.meatCount >= 5)
        {
            StartCoroutine(WinRoutine());
        }
    }

    private IEnumerator WinRoutine()
    {
        winUI.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        state = GameState.Paused;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pausePanel.SetActive(true);
    }

    public void UnPause()
    {
        state = GameState.Explore;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}