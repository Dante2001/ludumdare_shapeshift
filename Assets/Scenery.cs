using UnityEngine;
using System.Collections;

public class Scenery : MonoBehaviour {

	float depth;
	GameObject goPlayer;
	float prevPosition;
	// Use this for initialization
	void Start () {
		depth = 100f/GetComponent<SpriteRenderer> ().sortingOrder;//GetComponent<SpriteRenderer> ().sortingOrder/20f;
		goPlayer = GameObject.Find ("Player");
		prevPosition = goPlayer.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x + (goPlayer.transform.position.x - prevPosition)/(Mathf.Abs(depth)), transform.position.y, transform.position.z);
		prevPosition = goPlayer.transform.position.x;
	}
}
