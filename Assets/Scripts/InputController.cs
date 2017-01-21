using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	public GameObject sonarPrefab;
	public GameObject objectContainer;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			PlaceSensor(Input.mousePosition);
		}
	}

	private void PlaceSensor(Vector2 mousePosition)
	{
		Vector2 screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		GameObject newSensor = Instantiate(sonarPrefab, screenPosition, Quaternion.identity);
		newSensor.transform.SetParent(objectContainer.transform);
	}
}
