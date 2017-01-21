using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionFinder : MonoBehaviour
{
	private GameObject target;
	public Text distanceText;
	public Text directionText;

	void Start()
	{
		target = GameObject.FindWithTag("Target");

	}

	void Update()
	{
		var targetPos = target.transform.position;
		Vector2 heading = targetPos - transform.position;
		var distance = heading.magnitude;
		UpdateDebug(distance, heading);
	}

	private void UpdateDebug(float distance, Vector2 heading)
	{
		distanceText.text = distance.ToString();
		directionText.text = heading.ToString();
	}
}
