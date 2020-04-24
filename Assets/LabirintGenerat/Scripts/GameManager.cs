using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

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

    public int numberOfCoins = 3;

    public int currentNumberOfCoins = 0;


    public static UnityEvent CoinPickup;

    private void Awake()
    {
        instance = this;
        if (CoinPickup == null)
            CoinPickup = new UnityEvent();

        CoinPickup.AddListener(OnCoinPickup);
    }

    private void Start()
    {
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
        mazeInstance.Generate();
		mazeInstance.gameObject.name = "Maze";
        InstantiatePlayer();
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
        MazeCell randomPlayerCell = mazeInstance.GetRandomCell();
		playerInstance.transform.parent = gameArea;
        playerInstance.SetLocation(randomPlayerCell);
		playerInstance.gameObject.name = "Player";
    }

    private void OnCoinPickup()
    {
        currentNumberOfCoins += 1;
        if (currentNumberOfCoins == numberOfCoins)
        {
			mazeInstance.GenerateEndPoint();
			numberOfCoins +=1;
        }
    }
}