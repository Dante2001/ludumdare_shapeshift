using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {

	public float startSpeed;
    public float acceleration;
    private Rigidbody2D myRigidbody;
    private Vector3 currentVelocity;
    private bool onSlowDown;
    private AudioController audioController;
    private MeterController meterController;

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
        onSlowDown = false;
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        meterController = GameObject.Find("MeterController").GetComponent<MeterController>();
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
        UpdateSpeeds();
	}

    void UpdateSpeeds()
    {
        if (!onSlowDown)
        {
            currentVelocity.x = startSpeed * meterController.SanityPlayerSpeedMultiplier();
            myRigidbody.velocity = currentVelocity;
        }
        audioController.SetSpeedMultiplier(meterController.SanityAudioSpeedMultiplier());
    }

    void UnsuccessfulHit()
    {
        if (playerState == Transformation.HUMAN)
        {
            meterController.SanityDrainFaster();
        }
        else // playerState == Transformation.WEREWOLF
        {
            meterController.SanityDrainFaster();
        }
        CheckEndOfGame();

        //currentVelocity.x -= acceleration;
        if (currentVelocity.x <= 1f)
            currentVelocity.x = 1f;
        //set velocity to 0.4 for a second
        myRigidbody.velocity = Vector3.right * 0.4f;
        StartCoroutine("StartMoving");
        //do failure animation
        //Debug.Log("speed down");
    }

    void SuccessfulHit()
    {
        if (playerState == Transformation.HUMAN)
            meterController.SanityUp();
        else // playerState == Tranformation.WEREWOLF
            meterController.FullnessUp();

        //currentVelocity.x += acceleration;
        //set velocity to 0.4 for a second
        myRigidbody.velocity = Vector3.right * 0.4f;
        StartCoroutine("StartMoving");
        //do success animation
        //Debug.Log("speed up");
    }

    void CheckEndOfGame()
    {
        if (meterController.NoHealth())
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
        onSlowDown = true;
        yield return new WaitForSeconds(1.0f);
        onSlowDown = false;
        myRigidbody.velocity = currentVelocity;
    }
}
