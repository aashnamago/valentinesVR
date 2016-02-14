using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Heart : MonoBehaviour {

    public GameObject badDestructionPrefab;
    public GameObject goodDestructionPrefab;

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
        Text[] texts = GetComponentsInChildren<Text>();
        foreach(Text t in texts)
           t.text = data.body + "\n" + "From: " + data.city;
        sentiment = data.sentiment;
    }

    void OnTriggerEnter(Collider collide) {
		if (collide.tag != "Heart" && collide.tag != "Player")
        {
			if (sentiment > 0) {
				GameManager.Instance.incrementHealth ();
                Instantiate(goodDestructionPrefab, this.transform.position, Quaternion.identity);
			} else {
				GameManager.Instance.decrementHealth ();
                Instantiate(badDestructionPrefab, this.transform.position, Quaternion.identity);
            }
				
            Destroy(this.gameObject);
           
        }

        if (collide.tag == "Bonfire")
        {
            if (sentiment < 0)
            {
                GameManager.Instance.incrementHealth();
            } else
            {
                GameManager.Instance.decrementHealth();
            }

            Destroy(this.gameObject);
        }
    }

    public void SwitchLevel() {
        if (sentiment > .6)
            LevelManager.Instance.SwitchToScene(0);
        else if (sentiment > .2)
            LevelManager.Instance.SwitchToScene(1);
        else if (sentiment > -.2)
            LevelManager.Instance.SwitchToScene(2);
        else if (sentiment > -.6)
            LevelManager.Instance.SwitchToScene(3);
        else
            LevelManager.Instance.SwitchToScene(4);
    }
}
