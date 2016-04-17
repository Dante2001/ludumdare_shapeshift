using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class GameManager : MonoBehaviour {

	public GameObject goFirefly;
	public GameObject goRain;
	private GameObject goPlayer;


	ColorCorrectionCurves mColorCorrectionCurves;

	public SpriteRenderer day;
	public SpriteRenderer night;
	public Text daysSurvivedText;
	float timeOfDay = 0;
	int daysLived = 0;

	public void goBack() {
		Application.LoadLevel ("Menu");
	}

	// Use this for initialization
	void Start () {
		goPlayer = GameObject.Find ("Player");
		mColorCorrectionCurves = GetComponent<ColorCorrectionCurves> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		float nightAlpha = Mathf.Abs ((timeOfDay - 12f) / 12f);

		if (nightAlpha > .6f) {
			//its pretty dark, make fireflies!
			if (Random.Range (0, 100) > 98) {
				Instantiate (goFirefly, new Vector3 (goPlayer.transform.position.x +20,goPlayer.transform.position.y + Random.Range(-.5f,2f),goPlayer.transform.position.z), goFirefly.transform.rotation);
			}
		}

		Debug.Log (nightAlpha);

		mColorCorrectionCurves.redChannel = AnimationCurve.Linear (0, -nightAlpha * .5f, 1, 1f - ( nightAlpha * .5f));
		mColorCorrectionCurves.blueChannel = AnimationCurve.Linear(0, 0, 1, 1f);
		mColorCorrectionCurves.greenChannel = AnimationCurve.Linear(0, -nightAlpha * .2f, 1, 1f- ( nightAlpha * .2f) );
		mColorCorrectionCurves.UpdateParameters ();
		if (timeOfDay > 24) {
			timeOfDay = 0;
			daysLived++;
			daysSurvivedText.text = daysLived.ToString();
		}
		timeOfDay += 0.005f;
		//timeOfDay += 0.1f;

		night.color = new Color(1,1,1,nightAlpha );

	}
}
