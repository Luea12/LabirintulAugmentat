using UnityEngine;
using System.Collections;

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


	private void Awake()
	{
		instance = this;
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
		mazeInstance.transform.localPosition = new Vector3(0,0,0);
		mazeInstance.Generate();
		InstantiatePlayer();
	}

	private void RestartGame()
	{
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}

	private void InstantiatePlayer()
	{
		if(!playerInstance)
		{
			playerInstance = Instantiate(playerPrefab);
		}

		MazeCell randomPlayerCell = mazeInstance.GetRandomCell();

		playerInstance.SetLocation(randomPlayerCell);
	}
}