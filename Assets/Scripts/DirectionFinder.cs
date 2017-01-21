using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionFinder : MonoBehaviour
{
	public Text distanceText;
	public Text directionText;

	private GameObject target;
	private AudioManager audioManager;

	void Awake()
	{
		audioManager = GetComponent<AudioManager>();
	}

	void Start()
	{
		target = GameObject.FindWithTag("Target");
		AudioSource aSource = GetComponent<AudioSource>();

		//aSource.Play();
		DropSonar();
	}

	void Update()
	{
		//DropSonar();
	}

	private void DropSonar()
	{
		var targetPos = target.transform.position;
		Vector2 heading = targetPos - transform.position;
		var distance = heading.magnitude;
		UpdateDebug(distance, heading);
		//PlaySonar();
		PlayEcho(transform.position, targetPos);
	}

	private void UpdateDebug(float distance, Vector2 heading)
	{
		distanceText.text = distance.ToString();
		directionText.text = heading.ToString();
	}

	/// <summary>
	/// Initial ping sound when click occurs.
	/// </summary>
	private void PlaySonar()
	{
		// Set up multiple audio sources.
	}

	private void PlayEcho(Vector2 sourceVector, Vector2 targetVector)
	{
		var audioParams = audioManager.GetParams(sourceVector, targetVector, 960, 960);
		audioManager.AudioPlay(audioParams);
	}

}
