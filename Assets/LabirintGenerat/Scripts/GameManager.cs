using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    // Singleton
    private static GameManager _instance;
    public static GameManager instance
    {
        get { return _instance; }
        // Set only once (in the Awake method)
        private set
        {
            if (_instance != null)
            {
                return;
            }

            _instance = value;
        }
    }

    public Maze mazePrefab;

    public Maze mazeInstance
    {
        private set;
        get;
    }

    public PlayerController playerPrefab;

    public PlayerController playerInstance
    {
        private set;
        get;
    }

    public Transform gameArea;

    public Difficulty currentDifficulty;

    public int currentNumberOfCoins = 0;

    public int currentLevel = 0;

    public GameObject tempModel;

    private ProfileData profile;

    public void Awake()
    {
        instance = this;

        currentDifficulty = GlobalControl.instance.difficulties[GlobalControl.instance.selectedDifficultyIdx];
    }

    private void Start()
    {
        profile = ProfileData.Load();
        currentNumberOfCoins = profile.GetCoins();
        GameEvents.current.OnCoinPickup += OnCoinPickup;
        GameEvents.current.OnGameWin += OnGameWin;
        BeginGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    private void BeginGame()
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.transform.parent = gameArea;
        mazeInstance.transform.localPosition = new Vector3(0, 0, 0);

        int size = currentLevel*currentDifficulty.levelUpSize + currentDifficulty.startSize;
        mazeInstance.size = new IntVector2(size, size);

        mazeInstance.Generate();
		mazeInstance.gameObject.name = "Maze";
        InstantiatePlayer();
		gameArea.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void RestartGame()
    {
		currentNumberOfCoins = 0;
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
		Destroy(playerInstance.gameObject);
        BeginGame();
    }

    private void InstantiatePlayer()
    {
        playerInstance = Instantiate(playerPrefab);
        playerInstance.SetPlayerModel(tempModel);
        MazeCell randomPlayerCell = mazeInstance.GetRandomCell();
		playerInstance.transform.parent = gameArea;
        playerInstance.SetLocation(randomPlayerCell);
		playerInstance.gameObject.name = "Player";
    }

    private void OnCoinPickup()
    {
        currentNumberOfCoins += 1;
        if (currentNumberOfCoins == currentDifficulty.treshold)
        {
			mazeInstance.GenerateEndPoint();
        }
    }

    private void OnGameWin()
    {
        print("win");
    }
}