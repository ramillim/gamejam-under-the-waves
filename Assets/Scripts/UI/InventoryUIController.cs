using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
	public Text sonobouyCount;
	public Text depthChargesCount;

	void Awake()
	{
		Messenger.AddListener(GameEvent.ItemCountChanged, RefreshCounts);
	}

	void OnDestroy()
	{
		Messenger.RemoveListener(GameEvent.ItemCountChanged, RefreshCounts);
	}

	private void RefreshCounts()
	{
		sonobouyCount.text = GameManager.Instance.Board.ToString();
		depthChargesCount.text = GameManager.Instance.Board.ToString();
	}
}
