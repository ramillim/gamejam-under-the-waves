using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	private BoardManager board;

	void Start()
	{
		board = GameManager.Instance.Board;
	}


	void Update()
	{
		if (GameManager.Instance.GameState == GameState.Game){
			if (Input.GetMouseButtonDown(0))
			{
				board.PlaceSensor(Input.mousePosition);
			}
		}
	}
}
