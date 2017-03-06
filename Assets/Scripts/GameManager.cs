using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState
{
    Title,
    Instructions,
    Game,
    GameEndAnimation,
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
        Messenger.AddListener(GameEvent.GameLost, OnGameOver);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SubmarineHit, OnGameOver);
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

    public void GoToTitle()
    {
        IsWon = false;
        GameState = GameState.Title;
        SceneManager.LoadSceneAsync("Title");
    }

    private void OnGameOver()
    {
        Debug.Log("Game Over");
        if (Board.Submarine.GetComponent<Submarine>().IsHit)
        {
            IsWon = true;
            EndGame();
        }
        else
        {
            IsWon = false;
            LoadGameOverScreen();
        }

    }

    private void EndGame()
    {
        GameState = GameState.GameEndAnimation;
    }

    public void LoadGameOverScreen()
    {
        GameState = GameState.GameOver;
        SceneManager.LoadSceneAsync("GameOver");
    }
}
