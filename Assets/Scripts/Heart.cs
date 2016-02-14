using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Heart : MonoBehaviour {

    public int size;

    public GameObject destructionPrefab;

	private GameObject player;
    private float sentiment;

    // Use this for initialization
    void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
    }

    // Update is called once per frame
    void Update() {

	}

    public void SetSMSData(SMSManager.SMSData data) {
        GetComponentInChildren<Text> ().text = data.body + "\n" + "From: " + data.city;
        sentiment = data.sentiment;
    }

    void OnTriggerEnter(Collider collide) {
		if (collide.tag != "Heart" && collide.tag != "Player")
        {
			if (sentiment > 0) {
				GameManager.Instance.incrementHealth ();
			} else {
				GameManager.Instance.decrementHealth ();
			}
				
            Destroy(this.gameObject);
            Instantiate(destructionPrefab, this.transform.position, Quaternion.identity);
        }

    }
}
