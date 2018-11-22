using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hexagonScript : MonoBehaviour {

	public bool isBought = false;
	public bool isUpdated = false;

	public List<GameObject> surroundingSquares = new List<GameObject>();
	public bool canBeBought = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isBought && !isUpdated) {
			if (SceneManager.GetActiveScene ().name == "UIScene") {
				gameObject.GetComponent<SpriteRenderer> ().color = new Color (gameObject.GetComponent<SpriteRenderer> ().color.r, gameObject.GetComponent<SpriteRenderer> ().color.g, gameObject.GetComponent<SpriteRenderer> ().color.b, 0f);
				gameObject.GetComponent<Collider2D> ().enabled = false;
				for (int x = 0; x < surroundingSquares.Count; x++) {
					surroundingSquares [x].GetComponent<hexagonScript> ().canBeBought = true;
				}

				//GameObject localPlayer2 = GameObject.Find ("localPlayer2");
				/*if (localPlayer2 != null) {
					isUpdated = true;
					localPlayer2.GetComponent<networkPlayerScript> ().tileBought = gameObject.name;
					localPlayer2.GetComponent<networkPlayerScript> ().boughtATile = true;
				}*/
			}
		}
	}
}