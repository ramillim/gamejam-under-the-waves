using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionFinder : MonoBehaviour
{
	private GameObject target;
	private AudioManager audioManager;
	private AudioSource audioSource;

	void Awake()
	{
		audioManager = GetComponent<AudioManager>();
		audioSource = GetComponent<AudioSource>();
	}

	void Start()
	{
		target = GameObject.FindWithTag("Target");
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
		Messenger<float, Vector2>.Broadcast(GameEvent.DeploySonobouy, distance, heading);
		//PlaySonar();
		PlayEcho(transform.position, targetPos);
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
		var audioParams = audioManager.GetParams(sourceVector, targetVector, 9.6f, 9.6f);
		audioManager.AudioPlay(audioSource, audioParams);
	}
}
