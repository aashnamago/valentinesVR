using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    public float delayDest = 2f;

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(delayDest);
        Destroy(this.gameObject);
	}

}
