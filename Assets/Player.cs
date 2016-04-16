using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float startSpeed;
    public float acceleration;
	private Rigidbody2D myRigidbody;
    private Vector3 currentVelocity;
    private AudioController audioController;

    private enum Transformation
    {
        HUMAN,
        WEREWOLF
    };
    private Transformation playerState = Transformation.HUMAN;
    

	// Use this for initialization
	void Start () {
        myRigidbody = this.GetComponent<Rigidbody2D>();
        currentVelocity = myRigidbody.velocity;
        currentVelocity.x = startSpeed;
        myRigidbody.velocity = currentVelocity;
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        audioController.PlayAudio();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerState == Transformation.HUMAN)
            {
                playerState = Transformation.WEREWOLF;
                this.GetComponent<SpriteRenderer>().color = Color.red;
                audioController.SwapAudio();
            }
            else // playerState == Transformation.WEREWOLF
            {
                playerState = Transformation.HUMAN;
                this.GetComponent<SpriteRenderer>().color = Color.white;
                audioController.SwapAudio();
            }
        }

	}

    void DecreaseSpeed()
    {
        currentVelocity.x -= acceleration;
        myRigidbody.velocity = currentVelocity;
        audioController.SpeedDown();
        Debug.Log("speed down");
    }

    void IncreaseSpeed()
    {
        currentVelocity.x += acceleration;
        myRigidbody.velocity = currentVelocity;
        audioController.SpeedUp();
        Debug.Log("speed up");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "HumanObstacle" && playerState == Transformation.HUMAN)
            IncreaseSpeed();
        else if (col.tag == "HumanObstacle")
            DecreaseSpeed();
        else if (col.tag == "WerewolfObstacle" && playerState == Transformation.WEREWOLF)
            IncreaseSpeed();
        else if (col.tag == "WerewolfObstacle")
            DecreaseSpeed();

        if (col.tag.Contains("Obstacle"))
            col.GetComponent<BoxCollider2D>().enabled = false;
    }
}
