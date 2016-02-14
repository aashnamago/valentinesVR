using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public const float FadeIncrement = .01f;

	public List<GameObject> Forests;
	public int activeForest;

    public static LevelManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    // Use this for initialization
    void Start () {
		for(int i = 0; i < Forests.Count; i++) {
			if (Forests [i].activeInHierarchy)
				activeForest = i;
		}
	}
	
	// Update is called once per frame
	void Update () {
        //int currForest = (int) ( (float) GameManager.Instance.gameHealth / 20f);
        //Debug.LogError (GameManager.Instance.gameHealth);
        //SwitchToScene(currForest);
    }

	IEnumerator FadeForestOut(GameObject forest) {
		//forest.GetComponentInChildren<Light> ().enabled = true;

		Renderer[] rends = forest.GetComponentsInChildren<Renderer> ();
		for (float f = 1f; f >= 0f; f -= FadeIncrement){
			foreach(Renderer r in rends) {
				Color c = r.material.color;
				c.a = f;
				r.material.color = c;
			}

			yield return new WaitForFixedUpdate ();
		}

		//forest.GetComponentInChildren<Light> ().enabled = false;
		forest.SetActive (false);
	}

	IEnumerator FadeForestIn(GameObject forest) {
		//forest.GetComponentInChildren<Light> ().enabled = false;
		forest.SetActive (true);

		Renderer[] rends = forest.GetComponentsInChildren<Renderer> ();
		for (float f = 0f; f <= 1f; f += FadeIncrement){
			foreach(Renderer r in rends) {
				Color c = r.material.color;
				c.a = f;
				r.material.color = c;
			}

			yield return new WaitForFixedUpdate ();
		}

		//forest.GetComponentInChildren<Light> ().enabled = true;
	}

    public void SwitchToScene(int scene)
    {
        if (scene != activeForest)
        {
            StartCoroutine(FadeForestOut(Forests[activeForest]));
            StartCoroutine(FadeForestIn(Forests[scene]));
            activeForest = scene;
        }

    }
}
