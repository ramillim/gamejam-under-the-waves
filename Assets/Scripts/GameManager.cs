using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState
{
	Intro,
	Game,
	GameOver
}

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
	private BoardManager board;
	private bool isWon = false;

	[SerializeField]
	private GameState gameState;

	public static GameManager Instance
	{
		get
		{
			return instance;
		}
	}

	public BoardManager Board
	{
		get
		{
			return board;
		}

		private set
		{
			board = value;
		}
	}

	public GameState GameState
	{
		get
		{
			return gameState;
		}

		private set
		{
			gameState = value;
		}
	}

	public bool IsWon
	{
		get
		{
			return isWon;
		}

		private set
		{
			isWon = value;
		}
	}

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
			return;
		}

		board = GetComponent<BoardManager>();

		DontDestroyOnLoad(transform.gameObject);
	}

	void Start()
	{
		StartNewGame();
	}

	void Update()
	{
		switch (gameState)
		{
			case GameState.Intro:
				break;
			case GameState.Game:
				break;
			case GameState.GameOver:
				break;
		}
	}

	public void StartNewGame()
	{
		GameState = GameState.Game;
		SceneManager.LoadScene("Game");
		Board.ResetBoard();
	}

	private bool IsGameOver()
	{
		if (Board.DepthChargesRemaining <= 0)
		{

		}
	}

	private void WinGame()
	{
		IsWon = true;
	}

	private void LoseGame()
	{
		IsWon = false;
	}

	private void EndGame()
	{
		GameState = GameState.GameOver;
		SceneManager.LoadScene("Game Over");
	}
}
