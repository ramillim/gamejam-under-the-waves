using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
	[SerializeField]
	private Text distanceText;

	[SerializeField]
	private Text directionText;

	public Text DistanceText
	{
		get
		{
			return distanceText;
		}

		private set
		{
			distanceText = value;
		}
	}

	public Text DirectionText
	{
		get
		{
			return directionText;
		}

		private set
		{
			directionText = value;
		}
	}


	void Awake()
	{
		Messenger<float, Vector2>.AddListener(GameEvent.DeploySonobouy, OnDeploySonobouy);
	}

	void OnOnDestroy()
	{
		Messenger<float, Vector2>.RemoveListener(GameEvent.DeploySonobouy, OnDeploySonobouy);
	}

	private void OnDeploySonobouy(float distance, Vector2 heading)
	{
		distanceText.text = distance.ToString();
		directionText.text = heading.ToString();
	}
}
