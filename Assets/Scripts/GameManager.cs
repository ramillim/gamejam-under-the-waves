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

	private List<GameObject> sonobouyList = new List<GameObject>();

	[SerializeField]
	private GameState gameState;

	public static GameManager Instance
	{
		get
		{
			return instance;
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

		DontDestroyOnLoad(transform.gameObject);
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

	public void AddSonobouy(GameObject newSonobouy)
	{
		sonobouyList.Add(newSonobouy);
	}

	public void DestroySonobouys()
	{
		for (int i = 0; i < sonobouyList.Count; i++)
		{
			var sonoBouy = sonobouyList[i];
			sonobouyList.RemoveAt(i);
			Destroy(sonoBouy);
		}
	}
}
