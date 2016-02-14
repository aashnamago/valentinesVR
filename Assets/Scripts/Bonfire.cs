using UnityEngine;
using System.Collections;

public class Bonfire : MonoBehaviour {

    public GameObject Explosion;

	// Use this for initialization
	void OnTriggerEnter () {
        Instantiate(Explosion);
	}
}
