using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	private BoardManager board;
	public float nClickDoubleTime = 0.8f;
	public bool bClickChk;
	public float nTimer;

	void Start()
	{
		board = GameManager.Instance.Board;
	}


	void Update()
	{
		switch (GameManager.Instance.GameState)
		{
			case GameState.Intro:
				UpdateIntro();
				break;
			case GameState.Game:
				UpdateGame();
				break;
			case GameState.GameOver:
				UpdateGameOver();
				break;
		}
	}

	private void UpdateIntro()
	{
		if (Input.anyKey)
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
					board.SpawnMissile(Input.mousePosition);
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
				board.PlaceSensor(Input.mousePosition);
			}
		}
	}

	private void UpdateGameOver()
	{
		if (Input.anyKey)
		{
			GameManager.Instance.StartNewGame();
		}
	}
}
