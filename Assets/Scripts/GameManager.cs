using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
	Intro,
	Game,
	End
}

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
	private BoardManager board;

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
			case GameState.End:
				break;
		}
	}

	private void StartNewGame()
	{
		GameState = GameState.Game;
		Board.ResetBoard();
	}
}
