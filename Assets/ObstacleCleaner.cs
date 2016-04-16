using UnityEngine;
using System.Collections;

public class ObstacleCleaner : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.x > this.transform.position.x + 8)
        {
            Destroy(this.gameObject);
        }
	}
}
