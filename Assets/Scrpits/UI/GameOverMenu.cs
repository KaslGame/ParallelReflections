using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private PlayerRespawnManager _playerRespawnManager;
    [SerializeField] private GameObject _menu;

    private void OnEnable()
    {
        _playerRespawnManager.Died += OnDied;
    }

    private void OnDisable()
    {
        _playerRespawnManager.Died -= OnDied;
    }

    public void Again()
    {
        int gameScene = 0;

        Time.timeScale = 1;
        SceneManager.LoadScene(gameScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnDied(bool lastDeath)
    {
        if (lastDeath)
        {
            Time.timeScale = 0;
            _menu.SetActive(true);
        }
    }
}
