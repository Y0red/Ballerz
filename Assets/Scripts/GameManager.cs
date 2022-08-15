using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cinemachine;

public class GameManager : Manager<GameManager>
{
    #region Variables
    public enum GameState { PREGAME, PLAYING, PAUSE, MENU, LOGIN, GAMEOVER };

    public GameState currentGameState = GameState.MENU;

    public Events.EventGameState OnGameStateChanged;

    List<AsyncOperation> loadOperations;

    [SerializeField] private AssetReferenceGameObject[] SystemPrefabs;

    [SerializeField] List<GameObject> instanceSystemPrifabs;

    private string currentLevelName = string.Empty;

    [SerializeField] PlayerController_ZigZag player;
    [SerializeField] CinemachineVirtualCamera camera;

    public GameState CurrentGameState
    {
        get { return currentGameState; }
        private set { currentGameState = value; }

    }

    #endregion

    #region MonoBehaviours
    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private void Start()
    {

        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        DontDestroyOnLoad(gameObject);

        instanceSystemPrifabs = new List<GameObject>();

        loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrifabs();


        UpdateState(currentGameState);

    }

    protected void OnDestroy()
    {

        for (int j = 0; j < instanceSystemPrifabs.Count; j++)
        {
            //Addressables.Release(instanceSystemPrifabs[j].gameObject);
            //Destroy(instanceSystemPrifabs[j].gameObject);
        }
    }
    #endregion

    #region System Prifab Initialization
    void InstantiateSystemPrifabs()
    {
        GameObject prifabInstance;
        for (int i = 0; i < SystemPrefabs.Length; i++)
        {
            //Addressables.InstantiateAsync(SystemPrefabs[i]).Completed += (c) =>
            //{
              //  prifabInstance = c.Result.gameObject;
               // instanceSystemPrifabs.Add(prifabInstance);
           // };
            //prifabInstance = Instantiate(SystemPrefabs[i]);

            var op = Addressables.LoadAssetAsync<GameObject>(SystemPrefabs[i]);
            prifabInstance = op.WaitForCompletion();
            Instantiate(prifabInstance);
            instanceSystemPrifabs.Add(prifabInstance);

        }
    }

    #endregion
    public void StartGame(bool state)
    {
        ZigZagTileManager.Instance.stopORstart = state;
        
        player.enabled = true;
    }
    #region Scence Management
    public void UpdateState(GameState state)
    {
        GameState priviousGameState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1f;
                break;

            case GameState.PLAYING:
                Time.timeScale = 1f;
                break;

            case GameState.PAUSE:
                Time.timeScale = 0f;
                break;

            case GameState.MENU:
                Time.timeScale = 1f;
                break;
            case GameState.LOGIN:
                Time.timeScale = 1f;
                break;
            case GameState.GAMEOVER:
                Time.timeScale = 1f;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(currentGameState, priviousGameState);
    }
    
    public void LoadLevel(string levelName)
    {
        // AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        // if (ao == null)
        //  {
        //      Debug.LogError("[GameManager] unable to load level" + levelName);
        //      return;
        //  }
        //  ao.completed += OnLoadOperationComplate;
        //  loadOperations.Add(ao);
        //  currentLevelName = levelName;
    }
    public void UnloadLevel(string levelName)
    {
        // AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);

    }
    void OnLoadOperationComplate(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
            //transition to scene
            if (loadOperations.Count == 0)
            {

                //SenceMove(currentLevelName);

            }

        }

    }

    void OnUnLoadOperationComplate(AsyncOperation ao)
    {
        Debug.Log("UnLoad Complated");
    }
    public void TogglePause()
    {
        UpdateState(currentGameState == GameState.PLAYING ? GameState.PAUSE : GameState.PLAYING);
    }

    public void RestartGame()
    {
        // UpdateState(GameState.PREGAME);
        // ResetGame();
        Addressables.LoadSceneAsync("Assets/Scenes/SampleScene.unity", UnityEngine.SceneManagement.LoadSceneMode.Single).Completed += SortedSet => { };
    }
    public void GameOver()
    {
        //player.enabled = false;
        StopOrResumeCamera(null);

        StartCoroutine(WaitGameTime());

    }

    public void ResetGame()
    {
        PlayerController_ZigZag.Instance.ResetPosition();
        ZigZagTileManager.Instance.ResetPlatforms();
        StopOrResumeCamera(player.gameObject);
        UpdateState(GameState.PLAYING);
        StartGame(true);
    }
    void StopOrResumeCamera(GameObject gameObject = default)
    {
        camera.Follow = gameObject != null ? gameObject.transform : null;
    }
    IEnumerator WaitGameTime()
    {
        yield return new WaitForSeconds(2f);
        UpdateState(GameState.GAMEOVER);
        //ZigZagTileManager.Instance.ResetPlatforms();
        yield return null;
    }
    public void QuiteGame()
    {
        //autosaving
        //futures of quiting
        Application.Quit();
    }
    #endregion
}
