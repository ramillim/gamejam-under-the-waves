using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource nAudioSource;

    // public GameObject Gameobject?

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public MyParamsAudio GetParams(Vector2 v2SourceLoc, Vector2 v3TargetLoc, float nGridLengthX, float nGridLengthY)
    {
        // Default Var, can change
        float nDelayScalar = 5;
        float nPitchDefault = 5;
        float nPitchDelta = (float) 0.1;

        // Storing Source Var
        float nSourceX = v2SourceLoc.x;
        float nSourceY = v2SourceLoc.y;

        float nTargetX = v3TargetLoc.x;
        float nTargetY = v3TargetLoc.y;

        // Finding Deltas
        float nDeltaX = nTargetX - nSourceX;
        float nDeltaY = nTargetY - nSourceY;
        float nDistance = Mathf.Sqrt(Mathf.Pow(nDeltaX, 2) + Mathf.Pow(nDeltaY, 2));
        float nGridMaxLength = Mathf.Sqrt(Mathf.Pow(nGridLengthX, 2) + Mathf.Pow(nGridLengthY, 2));
        
        // Getting Params
        MyParamsAudio nParamAudio = new MyParamsAudio();
        nParamAudio.Pan = nDeltaX / nGridLengthX;
        nParamAudio.Delay = nDelayScalar* (nDistance / nGridMaxLength);
        nParamAudio.Pitch = nPitchDefault + nPitchDelta*(nDeltaY/nGridLengthY);

        return nParamAudio;
    }

    public void AudioPlay(float nPan = 0, float nPitch = 1, float nDelay = 0, float nVol = 1) 
    {
        nAudioSource.panStereo = nPan;
        nAudioSource.volume = nVol;
        nAudioSource.pitch = nPitch;

        nAudioSource.PlayDelayed(nDelay);
    }

    public void AudioPlay(MyParamsAudio nParams)
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
