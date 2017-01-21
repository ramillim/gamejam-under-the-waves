using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public float minBoardLimit = -4.8f;
	public float maxBoardLimit = 4.8f;
	public GameObject sonobouyPrefab;
	public GameObject submarinePrefab;
	public GameObject objectContainer;

	[SerializeField]
	private int maxDepthCharges = 3;

	[SerializeField]
	private int maxSonobouys = 10;

	private int sonobouysRemaining;
	private int depthChargesRemaining;
	private List<GameObject> sonobouyList = new List<GameObject>();

	public List<GameObject> SonobouyList
	{
		get
		{
			return sonobouyList;
		}

		set
		{
			sonobouyList = value;
		}
	}

	public int MaxDepthCharges
	{
		get
		{
			return maxDepthCharges;
		}

		set
		{
			maxDepthCharges = value;
		}
	}

	public int MaxSonobouys
	{
		get
		{
			return maxSonobouys;
		}

		set
		{
			maxSonobouys = value;
		}
	}

	public int DepthChargesRemaining
	{
		get
		{
			return depthChargesRemaining;
		}

		set
		{
			depthChargesRemaining = value;
		}
	}

	public int SonobouysRemaining
	{
		get
		{
			return sonobouysRemaining;
		}

		private set
		{
			sonobouysRemaining = value;
		}
	}

	// Use this for initialization
	void Start()
	{
		ResetBoard();
	}

	/// <summary>
	/// Initialize the board at the start of a new game.
	/// </summary>
	public void ResetBoard()
	{
		DestroySonobouys();
		PlaceSubmarine();
		SonobouysRemaining = MaxSonobouys;
		Messenger.Broadcast(GameEvent.ItemCountChanged);
	}

	public void AddSonobouy(GameObject newSonobouy)
	{
		SonobouyList.Add(newSonobouy);
	}

	public void PlaceSensor(Vector2 mousePosition)
	{
		if (SonobouysRemaining > 0)
		{
			DestroySonobouys();
			Vector2 screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);
			GameObject newSonobouy = Instantiate(sonobouyPrefab, screenPosition, Quaternion.identity);
			GameManager.Instance.Board.AddSonobouy(newSonobouy);
			newSonobouy.transform.SetParent(objectContainer.transform);
			sonobouysRemaining--;
			Messenger.Broadcast(GameEvent.ItemCountChanged);
		}
		else
		{
			// Play Empty Sound
		}
	}

	private void PlaceSubmarine()
	{
		var startingPosition = new Vector2(Random.Range(minBoardLimit, maxBoardLimit), Random.Range(minBoardLimit, maxBoardLimit));
		Instantiate(submarinePrefab, startingPosition, Quaternion.identity);
	}

	private void DestroySonobouys()
	{
		for (int i = 0; i < SonobouyList.Count; i++)
		{
			var sonoBouy = SonobouyList[i];
			SonobouyList.RemoveAt(i);
			Destroy(sonoBouy);
		}
	}
}
