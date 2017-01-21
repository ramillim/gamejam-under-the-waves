using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
	public Text sonobouyCount;
	public Text depthChargesCount;

	private BoardManager board;

	void Start()
	{
		board = GameManager.Instance.Board;
		Messenger.AddListener(GameEvent.ItemCountChanged, RefreshCounts);
	}

	void OnDestroy()
	{
		Messenger.RemoveListener(GameEvent.ItemCountChanged, RefreshCounts);
	}

	private void RefreshCounts()
	{
		sonobouyCount.text = board.SonobouysRemaining.ToString();
		depthChargesCount.text = board.DepthChargesRemaining.ToString();
	}
}
