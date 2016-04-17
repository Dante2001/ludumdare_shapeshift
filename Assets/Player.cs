using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {

	public float startSpeed;
    public float acceleration;
    public float taxPoints;
    public float sheepPoints;
    private Rigidbody2D myRigidbody;
    private Vector3 currentVelocity;

    private HeartBubbles sheepBubbles;
    private HeartBubbles taxBubbles;
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
        sheepBubbles = GameObject.Find("SheepPoints").GetComponent<HeartBubbles>();
        taxBubbles = GameObject.Find("TaxPoints").GetComponent<HeartBubbles>();
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

    void UnsuccessfulHit()
    {
        if (playerState == Transformation.HUMAN)
        {
            taxBubbles.LoseHeart();
            taxPoints -= 1;
        }
        else // playerState == Transformation.WEREWOLF
        {
            sheepBubbles.LoseHeart();
            sheepPoints -= 1;
        }
        CheckEndOfGame();

        currentVelocity.x -= acceleration;
        if (currentVelocity.x <= 1f)
            currentVelocity.x = 1f;
        audioController.SpeedDown();
        //set velocity to 0.4 for a second
        myRigidbody.velocity = Vector3.right * 0.4f;
        StartCoroutine("StartMoving");
        //do failure animation
        //Debug.Log("speed down");
    }

    void SuccessfulHit()
    {
        currentVelocity.x += acceleration;
        audioController.SpeedUp();
        //set velocity to 0.4 for a second
        myRigidbody.velocity = Vector3.right * 0.4f;
        StartCoroutine("StartMoving");
        //do success animation
        //Debug.Log("speed up");
    }

    void CheckEndOfGame()
    {
        if (taxPoints <= 0 || sheepPoints <= 0)
        {
            //some animation?
            //you lose screen
            //back to main menu
            SceneManager.LoadScene(0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "HumanObstacle" && playerState == Transformation.HUMAN)
            SuccessfulHit();
        else if (col.tag == "HumanObstacle")
            UnsuccessfulHit();
        else if (col.tag == "WerewolfObstacle" && playerState == Transformation.WEREWOLF)
            SuccessfulHit();
        else if (col.tag == "WerewolfObstacle")
            UnsuccessfulHit();

        if (col.tag.Contains("Obstacle"))
            col.GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator StartMoving()
    {
        // wait for 1 second then reset the velocity
        yield return new WaitForSeconds(1.0f);
        myRigidbody.velocity = currentVelocity;
    }
}
