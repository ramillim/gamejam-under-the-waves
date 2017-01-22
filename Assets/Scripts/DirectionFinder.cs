using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionFinder : MonoBehaviour
{
	private GameObject target;

    void Start()
	{
		target = GameObject.FindWithTag("Target");
		FindDirection();
	}

	private void FindDirection()
	{
		var targetPos = target.transform.position;
		Vector2 heading = targetPos - transform.position;
		var distance = heading.magnitude;
		Messenger<float, Vector2>.Broadcast(GameEvent.DeploySonobouy, distance, heading);
	}
}
