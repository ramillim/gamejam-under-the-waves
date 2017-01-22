using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	private BoardManager board;
    float nClickDoubleTime;
    bool bClickChk;
    float nTimer;

	void Start()
	{
		board = GameManager.Instance.Board;
        nClickDoubleTime = 0.8f;
	}


	void Update()
	{
		if (GameManager.Instance.GameState == GameState.Game){

			if (Input.GetMouseButtonDown(0))
			{
                if (bClickChk)
                {
                    if (Time.time - nTimer <= nClickDoubleTime)
                        board.SpawnMissile(Input.mousePosition);
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
	}
}
