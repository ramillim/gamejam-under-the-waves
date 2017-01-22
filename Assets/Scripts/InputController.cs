using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float depthChargeHoldButtonDuration = 0.8f;
    public float minHoldTimeForChargingSound = 0.3f;

    private BoardManager board;
    private Vector3 mousePosition;
    private float buttonPressTime;
    private bool isHolding;
    private bool isCharging;

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
            case GameState.GameEndAnimation:
                PressToLoadGameOver();
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

    private void PressToLoadGameOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.LoadGameOverScreen();
        }
    }

    private void UpdateGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            buttonPressTime = Time.time;
            isHolding = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            float holdTime = Time.time - buttonPressTime;

            if (holdTime > depthChargeHoldButtonDuration)
            {
                GameManager.Instance.Board.FireDepthCharge(Input.mousePosition);
            }
            else
            {
                GameManager.Instance.Board.PlaceSensor(Input.mousePosition);
            }

            isCharging = false;
            isHolding = false;
        }
        else if (!isCharging &&
                 isHolding &&
                 (Time.time - buttonPressTime > minHoldTimeForChargingSound))
        {
            isCharging = true;
            GameManager.Instance.Board.AudioPlayCharge();
        }
    }
}
