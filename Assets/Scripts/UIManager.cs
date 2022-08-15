using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject playMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TMP_Text text;


    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void FixedUpdate()
    {
        text.text = ZigZagTileManager.Instance.currentPlatform.ToString();
    }
    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState priviousState)
    {
        menu.SetActive(currentState == GameManager.GameState.MENU);

        pauseMenu.SetActive(currentState == GameManager.GameState.PAUSE);

        playMenu.SetActive(currentState == GameManager.GameState.PLAYING);

        gameOverMenu.SetActive(currentState == GameManager.GameState.GAMEOVER);
    }
    
}
