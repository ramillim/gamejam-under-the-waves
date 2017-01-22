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

    private GameObject depthCharge;

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

        if (Submarine)
        {
            Destroy(Submarine);
        }

        SonobouysRemaining = MaxSonobouys;
        DepthChargesRemaining = MaxDepthCharges;
        PlaceSubmarine();
        Messenger.Broadcast(GameEvent.ItemCountChanged);
    }

    public void SonarInstantiate(Vector2 screenPosition)
    {
        GameObject newSonobouy = Instantiate(sonobouyPrefab, screenPosition, Quaternion.identity);
        newSonobouy.transform.SetParent(transform);
    }

    IEnumerator SonarInstantiateMult(Vector2 screenPosition, float nRepeatRate)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newSonobouy = Instantiate(sonobouyPrefab, screenPosition, Quaternion.identity);
            newSonobouy.transform.SetParent(transform);
            yield return new WaitForSeconds(nRepeatRate);
        }
    }

    public void PlaceSensor(Vector2 mousePosition)
    {
        if (SonobouysRemaining > 0)
        {
            //if (lastSonobouy)
            //{
            //    Destroy(lastSonobouy);
            //}

            Vector2 screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            //for (int i = 0; i < 5; i++)
            //{
            //    //yield WaitForSeconds(0.2f);
            //    InvokeRepeating("SonarInstantiate(screenPosition)", 0.0f, 0.2f);

            //}
            //CancelInvoke();
            StartCoroutine(SonarInstantiateMult(screenPosition, 0.2f));
            //GameObject newSonobouy = Instantiate(sonobouyPrefab, screenPosition, Quaternion.identity);
            //// lastSonobouy = newSonobouy;
            //newSonobouy.transform.SetParent(transform);


            sonobouysRemaining--;
            Messenger.Broadcast(GameEvent.ItemCountChanged);

            MyParamsAudio nParams = nMAudio.GetParams(
                screenPosition, nLocSubmarine,
                maxBoardLimit - minBoardLimit,
                maxBoardLimit - minBoardLimit);
            nMAudio.AudioPlay(AudioManager.AudioType.Ping1, 0, 1, 0);
            nMAudio.AudioPlay(AudioManager.AudioType.Ping2, nParams, nParams.Delay);
        }
        else
        {
            // Play Empty Sound
            nMAudio.AudioPlay(AudioManager.AudioType.EmptySound);
        }
    }

    private void PlaceSubmarine()
    {
        nLocSubmarine = new Vector2(Random.Range(minBoardLimit, maxBoardLimit), Random.Range(minBoardLimit, maxBoardLimit));
        Submarine = Instantiate(submarinePrefab, nLocSubmarine, Quaternion.identity);
        Submarine.transform.SetParent(transform);
    }

    public void FireDepthCharge(Vector2 mousePosition)
    {
        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        nMAudio.AudioPlay(AudioManager.AudioType.Depthsplash, 0, 1, 0);

        depthChargesRemaining--;
        depthCharge = Instantiate(nPrefabMissile, screenPosition, Quaternion.identity);
        Destroy(depthCharge, 1.0f);
        Messenger.Broadcast(GameEvent.ItemCountChanged);

        if (IsHit(screenPosition, nLocSubmarine, nRadiusDefault))
        {
            nMAudio.AudioPlay(AudioManager.AudioType.SubKill, 0, 1,1);
            Submarine.GetComponent<Submarine>().RecordHit();
            Messenger.Broadcast(GameEvent.SubmarineHit);
        } 
    }

    public bool IsHit(Vector2 nLocSpawn, Vector2 nLocTarget, float nRadius = 0)
    {
        if (nRadius == 0)
        {
            nRadius = nRadiusDefault;
        }

        if (Vector2.Distance(nLocSpawn, nLocTarget) <= nRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
