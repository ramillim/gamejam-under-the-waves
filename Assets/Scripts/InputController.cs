using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	public GameObject sonobouyPrefab;
	public GameObject objectContainer;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameManager.Instance.DestroySonobouys();
			PlaceSensor(Input.mousePosition);
		}
	}

	private void PlaceSensor(Vector2 mousePosition)
	{
		Vector2 screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		GameObject newSonobouy = Instantiate(sonobouyPrefab, screenPosition, Quaternion.identity);
		GameManager.Instance.AddSonobouy(newSonobouy);
		newSonobouy.transform.SetParent(objectContainer.transform);
	}
}
