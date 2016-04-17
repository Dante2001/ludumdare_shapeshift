using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public GameObject goMenu;
	public GameObject goInstructions;
	public GameObject goCredits;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showCredits() {
		goMenu.SetActive (false);
		goInstructions.SetActive (false);
		goCredits.SetActive (true);

	}

	public void showInstructions() {
		goMenu.SetActive (false);
		goInstructions.SetActive (true);
		goCredits.SetActive (false);
	}

	public void goBack() {
		goMenu.SetActive (true);
		goInstructions.SetActive (false);
		goCredits.SetActive (false);
	}

	public void startGame() {
		Application.LoadLevel ("Game Dante");
	}
}
