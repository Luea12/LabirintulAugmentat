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

    public int currentDifficultyIdx;

    public int selectedGamemode;

    public int currentNumberOfCoins = 0;

    public int currentLevel = 0;

    public GameObject tempModel;

    public GameObject[] modelArray;

    private ProfileData profile;

    public void Awake()
    {
        instance = this;
        currentDifficultyIdx = GlobalControl.instance.selectedDifficultyIdx;
        currentDifficulty = GlobalControl.instance.difficulties[currentDifficultyIdx];
        selectedGamemode = GlobalControl.instance.selectedGamemode;
    }

    private void Start()
    {
        profile = ProfileData.Load();
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

        GameEvents.current.GameStart();
    }

    public void RestartGame()
    {
		currentNumberOfCoins = profile.GetCoins();
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
		Destroy(playerInstance.gameObject);
        BeginGame();
    }

    private void InstantiatePlayer()
    {
        playerInstance = Instantiate(playerPrefab);
        playerInstance.SetPlayerModel(modelArray[profile.GetCurrentCharacter()]);
        MazeCell randomPlayerCell = mazeInstance.GetRandomCell();
		playerInstance.transform.parent = gameArea;
        playerInstance.SetLocation(randomPlayerCell);
		playerInstance.gameObject.name = "Player";
    }

    private void OnCoinPickup()
    {
        AudioManager.instance.Play("CoinPickup");
        currentNumberOfCoins += 1;
        if (currentNumberOfCoins == currentDifficulty.treshold)
        {
			mazeInstance.GenerateEndPoint();
        }
    }

    private void OnGameWin()
    {
        profile.UpdateCoins(profile.GetCoins() + currentNumberOfCoins);
        profile.UnlockDifficulty(currentDifficultyIdx + 1);
    }
}