using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Vector3 SpawnPoint = Vector3.zero;
	public float SpawnRadius = 10f;

	//in seconds
	public float minWaitTime = .3f;
	public float maxWaitTime = 1.2f;

	public Heart HeartPrefab;


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
        SMSManager.SMSData data = new SMSManager.SMSData(0, "asdfa", "Hello World");
        //SMSManager.SMSData data = SMSManager.Instance.GetRandomSMS ();
        if (data != null) {
			Vector3 location = (new Vector3 (Random.Range (-1f, 1f), 0f, Random.Range (-1f, 1f))).normalized * SpawnRadius + SpawnPoint; 
			Heart heart = (Instantiate (HeartPrefab.gameObject, location, Quaternion.identity) as GameObject).GetComponent<Heart> ();
			heart.SetSMSData (SMSManager.Instance.GetRandomSMS ());
			return heart;
		}

		return null;
	}
}
