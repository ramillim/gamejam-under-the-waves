using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public float minBoardLimit = -4.8f;
	public float maxBoardLimit = 4.8f;

	public GameObject sonobouyPrefab;
	public GameObject submarinePrefab;
	public GameObject objectContainer;

	private List<GameObject> sonobouyList = new List<GameObject>();

    // Jon Added
    //public AudioClip nSoundPing;
    //public AudioClip nSoundEcho;
    Vector2 nLocSubmarine;
    private AudioManager nMAudio;
    public GameObject nGOaudioManager;


	public List<GameObject> SonobouyList
	{
		get
		{
			return sonobouyList;
		}

		set
		{
			sonobouyList = value;
		}
	}

	// Use this for initialization
	void Start()
	{
		ResetBoard();
        nMAudio = nGOaudioManager.GetComponent<AudioManager>();
	}

	/// <summary>
	/// Initialize the board at the start of a new game.
	/// </summary>
	public void ResetBoard()
	{
		DestroySonobouys();
		PlaceSubmarine();
	}

	public void AddSonobouy(GameObject newSonobouy)
	{
		SonobouyList.Add(newSonobouy);
	}

	public void PlaceSensor(Vector2 mousePosition)
	{
		DestroySonobouys();
		Vector2 screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		GameObject newSonobouy = Instantiate(sonobouyPrefab, screenPosition, Quaternion.identity);
		GameManager.Instance.Board.AddSonobouy(newSonobouy);
        newSonobouy.transform.SetParent(objectContainer.transform);

        // Playing Audio
        MyParamsAudio nParams =  nMAudio.GetParams(screenPosition, nLocSubmarine, maxBoardLimit - minBoardLimit, maxBoardLimit - minBoardLimit);
        nMAudio.PlayAudio1(nParams.Pan,0,0,1);
        nMAudio.PlayAudio2(nParams, nParams.Delay);
	}   

	private void PlaceSubmarine()
	{
		nLocSubmarine = new Vector2(Random.Range(minBoardLimit, maxBoardLimit), Random.Range(minBoardLimit, maxBoardLimit));
        Instantiate(submarinePrefab, nLocSubmarine, Quaternion.identity);
	}

	private void DestroySonobouys()
	{
		for (int i = 0; i < SonobouyList.Count; i++)
		{
			var sonoBouy = SonobouyList[i];
			SonobouyList.RemoveAt(i);
			Destroy(sonoBouy);
		}
	}
}
