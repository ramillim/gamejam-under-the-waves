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
	}

	public void AddSonobouy(GameObject newSonobouy)
	{
		SonobouyList.Add(newSonobouy);
	}

	public void PlaceSensor(Vector2 mousePosition)
	{
		DestroySonobouys();
		Vector2 screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		GameObject newSonobouy = Instantiate(sonobouyPrefab, screenPosition, Quaternion.identity);
		GameManager.Instance.Board.AddSonobouy(newSonobouy);
		newSonobouy.transform.SetParent(objectContainer.transform);
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
