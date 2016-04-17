using UnityEngine;
using System.Collections;

public class Scenery : MonoBehaviour {

	float depth;
	GameObject goPlayer;
	float prevPosition;
	// Use this for initialization
	void Start () {
		depth = GetComponent<SpriteRenderer> ().sortingOrder/20f;
		goPlayer = GameObject.Find ("Player");
		prevPosition = goPlayer.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x - (prevPosition - goPlayer.transform.position.x)/(Mathf.Abs(depth)), transform.position.y, transform.position.z);
		prevPosition = goPlayer.transform.position.x;
	}
}
