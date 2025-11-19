using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button instructions;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Instructions()
    {
        instructions.gameObject.SetActive(true);
    }

    public void HideInstructions()
    {
        instructions.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
