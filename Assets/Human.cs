using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {

	public Sprite deadState;

	public void die() {
		GetComponent<SpriteRenderer> ().sprite = deadState;
		this.transform.position = new Vector3 (transform.position.x, transform.position.y - .7f, transform.position.z);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
