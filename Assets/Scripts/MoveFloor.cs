using UnityEngine;
using System.Collections;

public class MoveFloor : MonoBehaviour {
	public string whichTag;
	public int size;
	GameObject goPlayer;
	private int mount = 0;
	// Use this for initialization
	void Start () {
		goPlayer = GameObject.Find ("Player");

		mount = GameObject.FindGameObjectsWithTag (whichTag).Length;

	}
	
	// Update is called once per frame
	void Update () {
		if (goPlayer.transform.position.x > this.transform.position.x+(size*(mount/2))){//+110) { 240
			this.transform.position = new Vector3(this.transform.position.x + (mount*size),this.transform.position.y,this.transform.position.z);
		}

	}
}
