#pragma strict

private var goPlayer : GameObject;

function Start () {
	goPlayer = GameObject.Find("Player");
}

function Update () {
	this.transform.position.x = goPlayer.transform.position.x+3;
}