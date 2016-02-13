using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SMSManager : MonoBehaviour {
	public const string ParseKey = "LoveNote2254";
	public const float PingWaitTime = 10f;

	public const string url = "http://stupidcupid.herokuapp.com/recent_notes";

	public static SMSManager Instance;

	private List<SMSData> unusedTexts = new List<SMSData>();

	public class SMSData {
		public float sentiment;
		public string city;
		public string body;

		public SMSData(float s, string c, string b) {
			sentiment = s;
			city = c;
			body = b;
		}
			
		public String ToString() {
			return "Sentiment: " + sentiment + "\n" + "City: " + city + "\n" + "SMS: " + body;
		}
	}

	void Awake() {
		if (Instance == null)
			Instance = this;

		//Test Script
		//ParseData (ParseKey + "5.3:BELMONT:ECKE #cv:h test lol\n jkWordpinch" + ParseKey + "6:SAN FRAN:sdf:asdf:sdfDFg'fasf\nsdfnbjsdfs");

		StartCoroutine (GetTextsPeriodically ());

	}

	void OnDestroy() {
		if (Instance == this)
			Instance = null;
	}

	IEnumerator GetTextsPeriodically() {
		while (true) {
			WWW www = new WWW (url);
			yield return www;
			yield return new WaitForFixedUpdate (); //align with update frame
			ParseData (www.text);
			yield return new WaitForSeconds (PingWaitTime);
		}
	}

	private void ParseData(string data) {
		string[] texts = data.Split(new string[] { ParseKey }, StringSplitOptions.None);
		for(int i = 1; i < texts.Length; i++) { //skip the first one empty space
			string text = texts [i];
			string[] messageSplit = text.Split (new char[] { ':' }, 3);
			SMSData sData = new SMSData (float.Parse (messageSplit [0]), messageSplit [1], messageSplit [2]);
			unusedTexts.Add (sData);
			Debug.LogError (sData.ToString());
		}
	}

	public SMSData GetRandomSMS () {
		if (unusedTexts.Count > 0) {
			int index = UnityEngine.Random.Range (0, unusedTexts.Count);
			SMSData sData = unusedTexts [index];
			unusedTexts.RemoveAt (index);
			return sData;
		}

		return null;
	}
}
