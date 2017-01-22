using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState
{
    Title,
    Instructions,
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

        DontDestroyOnLoad(transform.gameObject);

        Messenger.AddListener(GameEvent.SubmarineHit, OnGameOver);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SubmarineHit, OnGameOver);
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Title:
                break;
            case GameState.Instructions:
                break;
            case GameState.Game:
                if (IsGameOver())
                {
                    OnGameOver();
                }
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void SetBoardManager(BoardManager boardManager)
    {
        Board = boardManager;
    }

    public void LoadInstructions()
    {
        GameState = GameState.Instructions;
        SceneManager.LoadSceneAsync("Instructions");
    }

    public void StartNewGame()
    {
        IsWon = false;
        GameState = GameState.Game;
        SceneManager.LoadSceneAsync("Game");
    }

    private bool IsGameOver()
    {
        if (Board && Board.DepthChargesRemaining <= 0) // TODO: Implement other game over conditions
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnGameOver()
    {
        if (Board.Submarine.GetComponent<Submarine>().IsHit)
        {
            IsWon = true;
        }
        else
        {
            IsWon = false;
        }

        EndGame();
    }

    private void EndGame()
    {
        GameState = GameState.GameOver;
        SceneManager.LoadScene("Game Over");
    }
}
