using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton, leaderboardButton, exitButton, pauseButton, retryButton, mainMenuButton;
    void Start()
    {
        playButton.onClick.AddListener(OnPlay);
        pauseButton.onClick.AddListener(OnPause);
        exitButton.onClick.AddListener(OnExit);
        leaderboardButton.onClick.AddListener(OnRestart);
        retryButton.onClick.AddListener(OnRetry);
        mainMenuButton.onClick.AddListener(OnMainMenu);
    }

    private void OnMainMenu()
    {
        GameManager.Instance.UpdateState(GameManager.GameState.MENU);
        ZigZagTileManager.Instance.stopORstart = true;
    }

    private void OnRetry()
    {
        GameManager.Instance.ResetGame();
    }

    private void OnRestart()
    {
        GameManager.Instance.RestartGame();
    }

    private void OnExit()
    {
        GameManager.Instance.QuiteGame();
    }

    private void OnPause()
    {
        GameManager.Instance.UpdateState(GameManager.GameState.PAUSE);
    }

    private void OnPlay()
    {
        GameManager.Instance.UpdateState(GameManager.GameState.PLAYING);
        GameManager.Instance.StartGame(true);
    }
}
