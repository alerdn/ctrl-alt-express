using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameObject _menuPanel;

    private bool _isPaused;

    private void Start()
    {
        _inputReader.OnPauseEvent += PauseGame;

        _isPaused = false;
        _menuPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        _inputReader.OnPauseEvent -= PauseGame;
    }

    private void OnPauseGame()
    {
        if (!_isPaused)
        {
            PauseGame();
        }
        else
        {
            ContinueGame();
        }
    }

    public void PauseGame()
    {
        _isPaused = true;
        _menuPanel.SetActive(true);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ContinueGame()
    {
        _isPaused = false;
        _menuPanel.SetActive(false);
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
