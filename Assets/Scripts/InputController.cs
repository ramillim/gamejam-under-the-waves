using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	private BoardManager board;
	public float nClickDoubleTime = 0.8f;
	public bool bClickChk;
	public float nTimer;

	void Update()
	{
		switch (GameManager.Instance.GameState)
		{
			case GameState.Title:
                PressToLoadInstructions();
				break;
            case GameState.Instructions:
                PressToStart();
                break;
			case GameState.Game:
				UpdateGame();
				break;
			case GameState.GameOver:
                PressToStart();
				break;
		}
	}

    private void PressToLoadInstructions()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.LoadInstructions();
        }
    }

    private void PressToStart()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameManager.Instance.StartNewGame();
		}
	}

	private void UpdateGame()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (bClickChk)
			{
				if (Time.time - nTimer <= nClickDoubleTime)
				{
                    GameManager.Instance.Board.SpawnMissile(Input.mousePosition);
				}
			}
			else
			{
				bClickChk = true;
				nTimer = Time.time;
			}
		}

		// Restting if second click past timer, and then doing single click action
		if (bClickChk)
		{
			if (Time.time - nTimer > nClickDoubleTime)
			{
				bClickChk = false;
                GameManager.Instance.Board.PlaceSensor(Input.mousePosition);
			}
		}
	}
}
