using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionFinder : MonoBehaviour
{
	private GameObject target;
	public Text distanceText;
	public Text directionText;

	// Use this for initialization
	void Start()
	{
		target = GameObject.FindWithTag("Target");

	}

	// Update is called once per frame
	void Update()
	{
		var targetPos = target.transform.position;

		var heading = targetPos - transform.position;
		var distance = heading.magnitude;
		var direction = heading / distance;
		distanceText.text = distance.ToString();
		directionText.text = direction.ToString();

	}
}
