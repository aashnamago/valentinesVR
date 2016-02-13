using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Heart : MonoBehaviour {

	public int size;

	private float sentiment;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSMSData(SMSManager.SMSData data) {
		GetComponentInChildren<Text> ().text = data.body + "\n" + "From: " + data.city;
		sentiment = data.sentiment;
	}
}
