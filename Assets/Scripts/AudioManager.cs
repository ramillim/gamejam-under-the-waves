using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource nAudioSource1;
    private AudioSource nAudioSource2;
    public GameObject nGOAudio1;
    public GameObject nGOAudio2;

    void Awake()
    {
        nAudioSource1 = nGOAudio1.GetComponent<AudioSource>();
        nAudioSource2 = nGOAudio2.GetComponent<AudioSource>();
    }    

    public MyParamsAudio GetParams(Vector2 v2SourceLoc, Vector2 v2TargetLoc, float nGridLengthX, float nGridLengthY)
	{
		// Default Var, can change
		float nDelayScalar = 3f;
		float nPitchDefault = 0f;
		float nPitchDelta = 1f;
		float nMaxVol = 0.8f;
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

    public void PlayAudio1(MyParamsAudio nParams, float nDelay = 0)
    {
        AudioPlay(nAudioSource1, nParams, nDelay);
    }

    public void PlayAudio1(float nPan = 0, float nPitch = 1, float nDelay = 0, float nVol = 1)
    {
        AudioPlay(nAudioSource1, nPan, nPitch,nDelay,nVol);
    }

    public void PlayAudio2(MyParamsAudio nParams, float nDelay = 0)
    {
        AudioPlay(nAudioSource2, nParams, nDelay);
    }

    public void PlayAudio2(float nPan = 0, float nPitch = 1, float nDelay = 0, float nVol = 1)
    {
        AudioPlay(nAudioSource2,nPan, nPitch, nDelay, nVol);
    }


    public void AudioPlay(AudioSource nAudioSource, float nPan = 0, float nPitch = 1, float nDelay = 0, float nVol = 1)
	{
        //nAudioSource.clip = nAudioClip;
        nAudioSource.panStereo = nPan;
		nAudioSource.volume = nVol;
		//nAudioSource.pitch = nPitch;

		nAudioSource.PlayDelayed(nDelay);
	}

	public void AudioPlay(AudioSource nAudioSource, MyParamsAudio nParams, float nDelay = 0)
	{
        //nAudioSource.clip = nAudioClip;
        float nPan = nParams.Pan;
		float nPitch = nParams.Pitch;
		float nVol = nParams.Volume;

		nAudioSource.panStereo = nPan;
		nAudioSource.volume = nVol;
		//nAudioSource.pitch = nPitch;

        //Debug.Log(string.Format("Pan {0}, Pitch {1}, Delay {2}, Volume {3}",nPan,nPitch, nDelay,nVol));

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
