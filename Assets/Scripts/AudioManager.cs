using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource nAudioSourcePing1;
    public  AudioSource nAudioSourcePing2;
    public AudioSource nAudioSourceCharging;
    public AudioSource nAudioSourceDeadCity;
    public AudioSource nAudioSourceDepthSplash;
    public AudioSource nAudioSourceEmptySound;
    public AudioSource nAudioSourceGameStart;
    public AudioSource nAudioSourceSubKill;

    public enum AudioType
    {
        Ping1, Ping2, Charging, DeadCity, Depthsplash, EmptySound, GameStart, SubKill
    }

    void Awake()
    {
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
    

    public void AudioPlay(AudioType nAudioType, float nPan = 0,   float nVol = 1,float nDelay = 0)
	{
        AudioSource nAudioSource = GetAudioType(nAudioType);
        if (nAudioSource != null)
        {
            nAudioSource.panStereo = nPan;
            nAudioSource.volume = nVol;
            nAudioSource.PlayDelayed(nDelay);
        }

	}

	public void AudioPlay(AudioType nAudioType, MyParamsAudio nParams, float nDelay = 0)
	{
        AudioPlay(nAudioType, nParams.Pan, nParams.Volume, nDelay);
	}

    AudioSource GetAudioType(AudioType nAudioType)
    {
        AudioSource curAudio = null;
        switch (nAudioType)
        {
            case AudioType.Ping1:
                curAudio = nAudioSourcePing1;
                break;
            case AudioType.Ping2:
                curAudio = nAudioSourcePing2;
                break;
            case AudioType.Charging:
                curAudio = nAudioSourceCharging;
                break;
            case AudioType.DeadCity:
                curAudio = nAudioSourceDeadCity;
                break;
            case AudioType.Depthsplash:
                curAudio = nAudioSourceDepthSplash;
                break;
            case AudioType.EmptySound:
                curAudio = nAudioSourceEmptySound;
                break;
            case AudioType.GameStart:
                curAudio = nAudioSourceGameStart;
                break;
            case AudioType.SubKill:
                curAudio = nAudioSourceSubKill;
                break;
            default:
                break;
        }

        return curAudio;
    }

    public void AudioPlayCharge()
    {
        AudioPlay(AudioManager.AudioType.Charging);
    }
}

public class MyParamsAudio
{
	public float Pan;
	public float Volume;
	public float Pitch;
	public float Delay;
}
