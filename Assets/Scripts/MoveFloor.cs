using UnityEngine;
using System.Collections;

public class MoveFloor : MonoBehaviour {
	public string whichTag;
	public int size;
	GameObject goPlayer;
	private int mount = 0;
    
    //temp stuff for testing purposes
    public GameObject tHumanObs;
    public GameObject tWerewolfObs;

	// Use this for initialization
	void Start () {

		goPlayer = GameObject.Find ("Player");

		mount = GameObject.FindGameObjectsWithTag (whichTag).Length;

	}
	
	// Update is called once per frame
	void Update () {
		if (goPlayer.transform.position.x > this.transform.position.x+(size*(mount/2))){//+110) { 240
			this.transform.position = new Vector3(this.transform.position.x + (mount*size),this.transform.position.y,this.transform.position.z);
            
            int temp = Random.Range(0,8);
            if (temp == 0)
                Instantiate(tHumanObs, new Vector3(this.transform.position.x, -2.88f, 0f), Quaternion.identity);
            else if (temp == 7)
                Instantiate(tWerewolfObs, new Vector3(this.transform.position.x, -2.88f, 0f), Quaternion.identity);
		}

	}
}
