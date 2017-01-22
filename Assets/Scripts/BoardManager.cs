using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public float minBoardLimit = -4.8f;
	public float maxBoardLimit = 4.8f;
	public GameObject sonobouyPrefab;
	public GameObject submarinePrefab;

	private GameObject submarine;

	[SerializeField]
	private int maxDepthCharges = 3;

	[SerializeField]
	private int maxSonobouys = 10;

	private int sonobouysRemaining;
	private int depthChargesRemaining;
    private GameObject lastSonobouy;


    // Audio
    public GameObject nGOaudioManager;

	private Vector2 nLocSubmarine;
    private AudioManager nMAudio;

    // Missile (Depth Charge)
    public GameObject nPrefabMissile;
	public float nRadiusDefault = 0.2f;

	private GameObject nMissile;

	public int MaxDepthCharges
	{
		get
		{
			return maxDepthCharges;
		}

		set
		{
			maxDepthCharges = value;
		}
	}

	public int MaxSonobouys
	{
		get
		{
			return maxSonobouys;
		}

		set
		{
			maxSonobouys = value;
		}
	}

	public int DepthChargesRemaining
	{
		get
		{
			return depthChargesRemaining;
		}

		set
		{
			depthChargesRemaining = value;
		}
	}

	public int SonobouysRemaining
	{
		get
		{
			return sonobouysRemaining;
		}

		private set
		{
			sonobouysRemaining = value;
		}
	}

	public GameObject Submarine
	{
		get
		{
			return submarine;
		}

		private set
		{
			submarine = value;
		}
	}


    void Awake()
    {
        nMAudio = nGOaudioManager.GetComponent<AudioManager>();
        GameManager.Instance.SetBoardManager(this);
    }

	void Start()
	{
        ResetBoard();
    }

    /// <summary>
    /// Initialize the board at the start of a new game.
    /// </summary>
    public void ResetBoard()
	{
        if (lastSonobouy)
        {
            Destroy(lastSonobouy);
        }

        SonobouysRemaining = MaxSonobouys;
        DepthChargesRemaining = MaxDepthCharges;
		PlaceSubmarine();
        Messenger.Broadcast(GameEvent.ItemCountChanged);
    }

	public void PlaceSensor(Vector2 mousePosition)
	{
		if (SonobouysRemaining > 0)
		{
            if (lastSonobouy)
            {
                Destroy(lastSonobouy);
            }

            Vector2 screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);
			GameObject newSonobouy = Instantiate(sonobouyPrefab, screenPosition, Quaternion.identity);
            lastSonobouy = newSonobouy;
			newSonobouy.transform.SetParent(transform);
			sonobouysRemaining--;
			Messenger.Broadcast(GameEvent.ItemCountChanged);

			MyParamsAudio nParams = nMAudio.GetParams(
				screenPosition, nLocSubmarine,
			    maxBoardLimit - minBoardLimit,
			    maxBoardLimit - minBoardLimit);
			nMAudio.PlayAudio1(nParams.Pan, 0, 0, 1);
			nMAudio.PlayAudio2(nParams, nParams.Delay);
		}
		else
		{
			// Play Empty Sound
		}
	}

	private void PlaceSubmarine()
	{
		nLocSubmarine = new Vector2(Random.Range(minBoardLimit, maxBoardLimit), Random.Range(minBoardLimit, maxBoardLimit));
        Submarine = Instantiate(submarinePrefab, nLocSubmarine, Quaternion.identity);
	}

    public void SpawnMissile(Vector2 nLocSpawn)
    {
        nMissile = Instantiate(nPrefabMissile, nLocSpawn, Quaternion.identity);
        Destroy(nMissile, 1.0f);

		if (HitCheck(nLocSpawn, nLocSubmarine, nRadiusDefault))
        {
			Submarine.GetComponent<Submarine>().RecordHit();
            //GameOver();
        }
    }

    public bool HitCheck(Vector2 nLocSpawn, Vector2 nLocTarget, float nRadius = 0)
    {
        if (nRadius == 0)
            nRadius = nRadiusDefault;

        if (Vector2.Distance(nLocSpawn, nLocTarget) <= nRadius)
            return true;

        return false;
    }
}
