using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
	private bool isHit = false;

	public bool IsHit
	{
		get
		{
			return isHit;
		}

		private set
		{
			isHit = value;
		}
	}

	public void RecordHit()
	{
		IsHit = true;
	}
}
