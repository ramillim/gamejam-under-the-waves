using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public MyParamsAudio GetParams(Vector2 v2SourceLoc, Vector2 v2TargetLoc, float nGridLengthX, float nGridLengthY)
	{
		// Default Var, can change
		float nDelayScalar = 5f;
		float nPitchDefault = 5f;
		float nPitchDelta = 0.1f;
		float nMaxVol = 1.0f;
		float nMinVol = 0f;

		// Storing Source Var
		float nSourceX = v2SourceLoc.x;
		float nSourceY = v2SourceLoc.y;

		float nTargetX = v2TargetLoc.x;
		float nTargetY = v2TargetLoc.y;

		// Finding Deltas
		float nDeltaX = nTargetX - nSourceX;
		float nDeltaY = nTargetY - nSourceY;
		float nDistance = Mathf.Sqrt(Mathf.Pow(nDeltaX, 2) + Mathf.Pow(nDeltaY, 2));
		float nGridMaxLength = Mathf.Sqrt(Mathf.Pow(nGridLengthX, 2) + Mathf.Pow(nGridLengthY, 2));

		// Getting Params
		var nParamAudio = new MyParamsAudio();
		nParamAudio.Pan = nDeltaX / nGridLengthX;
		nParamAudio.Delay = nDelayScalar * (nDistance / nGridMaxLength);
		nParamAudio.Pitch = nPitchDefault + nPitchDelta * (nDeltaY / nGridLengthY);
		nParamAudio.Volume = nMaxVol - (nMaxVol - nMinVol) * (nDistance / nGridMaxLength);

		return nParamAudio;
	}

	public void AudioPlay(AudioSource nAudioSource, float nPan = 0, float nPitch = 1, float nDelay = 0, float nVol = 1)
	{
		nAudioSource.panStereo = nPan;
		nAudioSource.volume = nVol;
		nAudioSource.pitch = nPitch;

		nAudioSource.PlayDelayed(nDelay);
	}

	public void AudioPlay(AudioSource nAudioSource, MyParamsAudio nParams)
	{
		float nPan = nParams.Pan;
		float nPitch = nParams.Pitch;
		float nDelay = nParams.Delay;
		float nVol = nParams.Volume;

		nAudioSource.panStereo = nPan;
		nAudioSource.volume = nVol;
		nAudioSource.pitch = nPitch;

		nAudioSource.PlayDelayed(nDelay);
	}
}

public class MyParamsAudio
{
	public float Pan;
	public float Volume;
	public float Pitch;
	public float Delay;
}
