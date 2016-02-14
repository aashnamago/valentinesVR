using UnityEngine;
using System.Collections;

public class StareAtPlayer : MonoBehaviour {
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion rot = Quaternion.LookRotation (player.transform.position - transform.position);

		transform.localEulerAngles = new Vector3(
			0.0f,
			rot.eulerAngles.y,
			0.0f
		);
	}
}
