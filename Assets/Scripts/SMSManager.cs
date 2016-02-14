using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SMSManager : MonoBehaviour {
	public const string ParseKey = "LoveNote2254";
	public const float PingWaitTime = 10f;

	public const string url = "http://stupidcupid.herokuapp.com/recent_notes";
	string[] stock_valentines = new string[] {
		"LoveNote22540:-0.3:Palo Alto:You are my third favorite mammal after dogs and cats...", 
		"LoveNote22540:-0.5:San Francisco:You're only good for your Netflix account.",
		"LoveNote22540:-0.1:Stanford:I don't dislike you.",
		"LoveNote22540:-0.1:Menlo Park:Beggars can't be choosers.",
		"LoveNote22540:0.0:Redwood City:Netflix and chill?.",
		"LoveNote22540:0.9:Stanford:I wish I could feel the magic of meeting you again every day",
		"LoveNote22540:0.7:Stanford:You are important to me",
		"LoveNote22540:0.5:Stanford:In your smile I see something more beautiful than the stars"
	};


	public static SMSManager Instance;

	private HashSet<int> usedRealNoteIDs = new HashSet<int> ();
	private List<SMSData> realNotes = new List<SMSData>();
	private List<SMSData> stockNotes = new List<SMSData>();


	public class SMSData {
		public int id;
		public float sentiment;
		public string city;
		public string body;

		public SMSData(int i, float s, string c, string b) {
			id = i;
			sentiment = s;
			city = c;
			body = b;
		}
			
		public String ToString() {
			return "ID: " + id + "\n" + "Sentiment: " + sentiment + "\n" + "City: " + city + "\n" + "SMS: " + body;
		}
	}

	void Awake() {
		if (Instance == null)
			Instance = this;

		for (int i = 0; i < stock_valentines.Length; i++) {
			ParseData (stock_valentines [i], false);
		}

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
			ParseData (www.text, true);
			yield return new WaitForSeconds (PingWaitTime);
		}
	}

	private void ParseData(string data, bool skipDuplicates) {
		string[] texts = data.Split(new string[] { ParseKey }, StringSplitOptions.None);
		for(int i = 1; i < texts.Length; i++) { //skip the first one empty space
			string text = texts [i];
			string[] messageSplit = text.Split (new char[] { ':' }, 4);
			SMSData sData = new SMSData (int.Parse(messageSplit[0]), float.Parse (messageSplit [1]), messageSplit [2], messageSplit [3]);
			if (skipDuplicates && usedRealNoteIDs.Contains (sData.id))
				continue;
			
			usedRealNoteIDs.Add (sData.id);
			stockNotes.Add (sData);
		}
	}

	public SMSData GetRandomSMS () {
		if (realNotes.Count > 0) {
			int index = UnityEngine.Random.Range (0, realNotes.Count-1);
			SMSData sData = realNotes [index];
			stockNotes.RemoveAt (index);
			return sData;
		} else if (stockNotes.Count > 0) {
			int index = UnityEngine.Random.Range (0, stockNotes.Count-1);
			SMSData sData = stockNotes [index];
			stockNotes.RemoveAt (index);
			return sData;
		}
		return null;
	}
}
