using UnityEngine;
using System.Collections;

public class SetHeartMaterial : MonoBehaviour {
    public Material[] materials;

	// Use this for initialization
	void Start () {
        GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
