using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float nClickDoubleTime = 0.8f;

	private BoardManager board;
	private Vector3 mousePosition;
	private float nTimer;
	private bool hasFirstClick;

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
			mousePosition = Input.mousePosition;

			if (hasFirstClick)
            {
                if (Time.time - nTimer <= nClickDoubleTime)
                {
                    GameManager.Instance.Board.FireDepthCharge(mousePosition);
                }
            }
            else
            {
                hasFirstClick = true;
                nTimer = Time.time;
            }
        }

        // Restting if second click past timer, and then doing single click action
        if (hasFirstClick)
        {
            if (Time.time - nTimer > nClickDoubleTime)
            {
                hasFirstClick = false;
                GameManager.Instance.Board.PlaceSensor(mousePosition);
            }
        }
    }
}
