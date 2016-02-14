using UnityEngine;
using System.Collections;

public class Bonfire : MonoBehaviour {

    public GameObject Explosion;

	// Use this for initialization
	void OnTriggerEnter () {

        Instantiate(Explosion, new Vector3(0f, 2f, 0f) + transform.position , Quaternion.identity);
	}
}
