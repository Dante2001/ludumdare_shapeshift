using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public GameObject particleSuccessTax;
	public GameObject particleSuccessSheep;
	public GameObject particleFailTax;
	public GameObject particleFailSheep;

	public GameObject spTaxesDone;
	public GameObject spVictimMauled;
	public GameObject goSmokeBomb;

	public float startSpeed;
    public float acceleration;
    private Rigidbody2D myRigidbody;
    private Vector3 currentVelocity;
    private bool onSlowDown;
    private AudioController audioController;
    private MeterController meterController;

    private GameObject fido;
    private GameObject frida;

    private Animator fidoAnimator;
    private Animator fridaAnimator;
    //private Animator transformationAnimator;
    private Animator currentAnimator;

    private bool isGameOver = false;

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
        fido = GameObject.Find("Fido");
        frida = GameObject.Find("Frida");
        fidoAnimator = fido.GetComponent<Animator>();
        fridaAnimator = frida.GetComponent<Animator>();
        currentAnimator = fridaAnimator;
        fido.SetActive(false);
        audioController.PlayAudio();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Space))
        {
			GameObject instantiatedSmoke = (GameObject) Instantiate (goSmokeBomb, new Vector3(transform.position.x, transform.position.y-.4f, transform.position.z), goSmokeBomb.transform.rotation);
			instantiatedSmoke.transform.SetParent (transform);
            if (playerState == Transformation.HUMAN)
            {
                playerState = Transformation.WEREWOLF;
                fido.SetActive(true);
                currentAnimator = fidoAnimator;
                currentAnimator.SetFloat("RunMultiplier", meterController.TimeAudioSpeedMultiplier());
                frida.SetActive(false);
                audioController.SwapAudio();
            }
            else // playerState == Transformation.WEREWOLF
            {
                playerState = Transformation.HUMAN;
                frida.SetActive(true);
                currentAnimator = fridaAnimator;
                currentAnimator.SetFloat("RunMultiplier", meterController.TimeAudioSpeedMultiplier());
                fido.SetActive(false);
                audioController.SwapAudio();
            }
        }
        if (!isGameOver)
            UpdateSpeeds();
	}

    void UpdateSpeeds()
    {
		if (currentVelocity.x < 20) {
			currentVelocity.x = startSpeed * meterController.TimePlayerSpeedMultiplier ();
			//Run Multiplier also increases the speed of the "action" since we don't slow down now
			currentAnimator.SetFloat("RunMultiplier", meterController.TimePlayerSpeedMultiplier());
			audioController.SetSpeedMultiplier(meterController.TimeAudioSpeedMultiplier());
		}

        myRigidbody.velocity = currentVelocity;
    }

    void UnsuccessfulHit()
    {
        // missed taxes and sanity goes down a bit
		if (playerState == Transformation.WEREWOLF)
			meterController.SanityDrainFaster();
		if (playerState == Transformation.HUMAN)
			meterController.SanityDrainFaster();
		CheckEndOfGame();
    }

    void SuccessfulHit()
    {

        currentAnimator.SetTrigger("ToAction");
        if (playerState == Transformation.HUMAN)
        {
            meterController.SanityUp();
            StartCoroutine(StartRuningAfter(0.6f / meterController.TimePlayerSpeedMultiplier()));
        }
        else // playerState == Tranformation.WEREWOLF
        {
            meterController.FullnessUp();
            //meterController.SanityDrainFaster();
            StartCoroutine(StartRuningAfter(0.6f / meterController.TimePlayerSpeedMultiplier()));
        }
    }

    void CheckEndOfGame()
    {
        if (meterController.NoHealth())
        {
            //some animation?
            //you lose screen
            //back to main menu
            isGameOver = true;
            StartCoroutine(GameOver());
            
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.tag == "HumanObstacle" && playerState == Transformation.HUMAN) {
			SuccessfulHit ();
			Instantiate (particleSuccessTax, col.transform.position, particleSuccessTax.transform.rotation);
			Instantiate (spTaxesDone, new Vector3(col.transform.position.x,col.transform.position.y+2.5f,col.transform.position.z), spTaxesDone.transform.rotation);

		} else if (col.tag == "HumanObstacle") {
			UnsuccessfulHit ();
//			Instantiate (particleFailSheep, transform.position, particleFailSheep.transform.rotation);
		}
		else if (col.tag == "WerewolfObstacle" && playerState == Transformation.WEREWOLF) {
			SuccessfulHit ();
			col.gameObject.GetComponent<Human> ().die ();
			Instantiate (particleSuccessSheep, transform.position, particleSuccessSheep.transform.rotation);
			Instantiate (spVictimMauled, new Vector3(col.transform.position.x,col.transform.position.y+2.5f,col.transform.position.z), spVictimMauled.transform.rotation);
		} else if (col.tag == "WerewolfObstacle") {
			UnsuccessfulHit ();
//			Instantiate (particleFailTax, transform.position, particleFailTax.transform.rotation);
		}
        if (col.tag.Contains("Obstacle"))
            col.GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator GameOver()
    {
        audioController.DeaccelerateOnGameover();
        //smoke explosion here
        fido.SetActive(false);
        frida.SetActive(false);
        //display game over overlay
        float[] temp = meterController.GetMeterValues();
        if (temp[0] == 0 && temp[1] == 0)
            GameObject.Find("Gameover both").GetComponent<Image>().enabled = true;
        else if (temp[0] == 0)
            GameObject.Find("Gameover sanity").GetComponent<Image>().enabled = true;
        else
            GameObject.Find("Gameover fullness").GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }

    IEnumerator StartRuningAfter(float waitTime = 1.0f)
    {
        // wait for for time then reset the velocity
        yield return new WaitForSeconds(waitTime);
        currentAnimator.SetTrigger("ToRun");
    }
}
