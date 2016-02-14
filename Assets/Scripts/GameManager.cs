using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Vector3 SpawnPoint = Vector3.zero;
	public float SpawnRadius = 10f;

	[HideInInspector]
	public int gameHealth = 0;
	private int incrementAmount = 5;
	private int decrementAmount = -5;

	//in seconds
	public float minWaitTime = .3f;
	public float maxWaitTime = 1.2f;

	public GameObject HeartPrefab;

	public static GameManager Instance;

	void Awake() {
		if (Instance == null)
			Instance = this;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnHearts ());
	}
	
	IEnumerator SpawnHearts() {
		while (true) {
			CreateHeart ();
			yield return new WaitForSeconds (Random.Range(minWaitTime, maxWaitTime));
		}
	}

	public Heart CreateHeart() {
        //SMSManager.SMSData data = new SMSManager.SMSData(0, "asdfa", "Hello World");
        SMSManager.SMSData data = SMSManager.Instance.GetRandomSMS ();
        if (data != null) {
			Vector3 location = (new Vector3 (Random.Range (-1f, 1f), 0f, Random.Range (-1f, 1f))).normalized * SpawnRadius + SpawnPoint; 
			Heart heart = (Instantiate (HeartPrefab, location, Quaternion.identity) as GameObject).GetComponentInChildren<Heart> ();
			heart.SetSMSData (data);
			return heart;
		}
		return null;
	}

	public void decrementHealth() {
		gameHealth += decrementAmount;
		if (gameHealth < 0)
			gameHealth = 0;
	}

	public void incrementHealth() {
		gameHealth += incrementAmount;
		if (gameHealth > 100)
			gameHealth = 100;
	}
}
