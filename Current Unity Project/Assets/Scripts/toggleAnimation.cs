using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleAnimation : MonoBehaviour {

	//string animController;
	//float animSpeed;
	// Use this for initialization
	void Start () {
		//animController = gameObject.GetComponent<Animator>().runtimeAnimatorController.name;
		//animSpeed = gameObject.GetComponent<Animator> ().speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gameManager.GetComponent<GameManager> ().waveOngoing) {
			//gameObject.GetComponent<Animator> ().Play (animController, 0, 0f);
			gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			gameObject.GetComponent<Animator> ().speed = 1f;
		} else {
			//gameObject.GetComponent<Animator> ().Play (animController, 0, 0f);
			gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
			gameObject.GetComponent<Animator> ().speed = 0f;
		}
	}
}