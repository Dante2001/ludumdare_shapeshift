using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {

	public float destroyTime = 3f;

	// Use this for initialization
	void Start () {
		Invoke ("destroyMyself", destroyTime);
	}


	public void destroyMyself() {
		Destroy (this.gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
