using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{


    public delegate void PlayerInstanceDelegate(PlayerController player);
    public event PlayerInstanceDelegate OnPlayerSpawned;

    #region Singleton Pattern

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);

    }

    #endregion

    #region Life Managment
    private int _lives = 3;
    private int maxLives = 5;

    public int Lives
    {
        get => _lives;
        set
        {
            if (value < 0)
            {
                GameOver();
               
                SceneManager.LoadScene("GameOver");
                return;
            }

            if (_lives > value)
            {
                Respawn();
            }
            _lives = value;
            if (value > maxLives)
            {
                _lives = maxLives;
            }
            Debug.Log("life value change to:" + _lives);
        }
    }
    #endregion

    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController PlayerInstance => _playerInstance;
    private Vector3 currentCheckpoint;

 
    // Update is called once per frame
    void Update()
    {
 
        //Debug toggle to go between scenes
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            string sceneToLoad = currentSceneName == "Title" ? "Game" : "Title";
       
            SceneManager.LoadScene(sceneToLoad);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Lives++;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Lives--;
        }
    }
    public void SpawnPlayer(Vector3 spawnPos)
    {
        _playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        UpdateCheckpoint(spawnPos);

        OnPlayerSpawned?.Invoke(_playerInstance);
    
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint) => currentCheckpoint = newCheckpoint;

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }
    private void Respawn()
    {
        _playerInstance.transform.position = currentCheckpoint;
    }
    public void ResetGame()
    {
        _lives = 3;
        _playerInstance = null;
    }
}


