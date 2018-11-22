using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;


public class healthBombScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {

	float timeToBuy = 0.15f;
	float currenTime = 0.0f;
	public int bombPrice;
	public int healthPrice; 

	public int healthPackHealing = 25;

	public GameObject healthPriceText;
	public GameObject bombPriceText;

	bool startTimer = false;

	GameObject theCanvas;
	GameObject UIHolder;
	GameObject activeMap;
	GameObject bombText;

	// Use this for initialization
	void Start () {
		healthPriceText.GetComponent<Text> ().text = "$ " + healthPrice.ToString();
		bombPriceText.GetComponent<Text> ().text = "$ " + bombPrice.ToString ();
		theCanvas = GameObject.Find ("Canvas");
		UIHolder = GameObject.Find ("UIHolder");
		activeMap = GameObject.Find ("activeEnemy");
		bombText = theCanvas.transform.Find ("bombText").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (startTimer) {
			currenTime += Time.deltaTime;
		}

		if (GameManager.gameManager.GetComponent<GameManager> ().Resource < bombPrice && gameObject.name == "bombButton") {
			gameObject.transform.parent.GetChild (2).GetComponent<Image> ().color = new Color (139f / 255f, 84f / 255f, 84f / 255f, 1f);
			gameObject.GetComponent<Image> ().color = new Color (139f / 255f, 84f / 255f, 84f / 255f, 1f);
		} else if(gameObject.name == "bombButton"){
			gameObject.transform.parent.GetChild (2).GetComponent<Image> ().color = Color.white;
			gameObject.GetComponent<Image> ().color = Color.white;
		}

		if (GameManager.gameManager.GetComponent<GameManager> ().Resource < healthPrice && gameObject.name == "healthButton") {
			gameObject.transform.parent.GetChild (2).GetComponent<Image> ().color = new Color (139f / 255f, 84f / 255f, 84f / 255f, 1f);
			gameObject.GetComponent<Image> ().color = new Color (139f / 255f, 84f / 255f, 84f / 255f, 1f);
		} else if(gameObject.name == "healthButton"){
			gameObject.transform.parent.GetChild (2).GetComponent<Image> ().color = Color.white;
			gameObject.GetComponent<Image> ().color = Color.white;
		}
	}
		
	public void OnPointerEnter(PointerEventData eventData)
	{
		startTimer = true;
		gameObject.GetComponent<Outline> ().enabled = true;
		gameObject.transform.parent.GetChild (0).gameObject.SetActive (true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		gameObject.transform.parent.GetChild (0).gameObject.SetActive (false);
		gameObject.GetComponent<Outline> ().enabled = false;
	}

	public void buyHealth()
	{
		if (currenTime > timeToBuy) {
		} else {
			if (GameManager.gameManager.GetComponent<GameManager> ().Resource >= healthPrice && !GameManager.gameManager.GetComponent<GameManager>().isFullHealth && !GameManager.gameManager.GetComponent<GameManager>().isDead) {
				// have a check to see if P2 is not full health
				// if he is not, then add in some amount of health
				// in the game scene, then add in the full amount or until he is back to full health
				Debug.Log ("Bought health pack");
				GameObject localPlayer2 = GameObject.Find ("localPlayer2");
				localPlayer2.GetComponent<networkPlayerScript> ().player2HealAmount = healthPackHealing;
				localPlayer2.GetComponent<networkPlayerScript> ().increasePlayer2Health = true;
				GameManager.gameManager.GetComponent<GameManager> ().Resource -= healthPrice;
			}
		}
		startTimer = false;
		currenTime = 0.0f;
	}

	public void buyBomb()
	{
		if (currenTime > timeToBuy) {
		} else {
			if (GameManager.gameManager.GetComponent<GameManager> ().Resource >= bombPrice) {
				GameManager.gameManager.GetComponent<GameManager> ().Resource -= bombPrice;
				// disable shop
				// player then has text that pops up saying "Press a location to place a bomb!"
				Debug.Log ("Bought bomb");
				bombText.GetComponent<Text> ().text = "Place the bomb";
				activeMap.transform.Find ("enemyPath").gameObject.SetActive (true);

				GameManager.gameManager.GetComponent<GameManager> ().shopOpen = false;
				var turrets = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.tag == "turret");

				foreach(GameObject theTurrets in turrets)
				{
					theTurrets.SetActive (true);
				}

				theCanvas.transform.Find ("PlayButton").gameObject.SetActive (true);
				theCanvas.transform.Find ("shopButton").gameObject.SetActive (true);
				UIHolder.transform.Find ("shopPanel").gameObject.SetActive (false);
				UIHolder.transform.Find ("shopBackground").gameObject.SetActive (false);
				GameManager.gameManager.GetComponent<GameManager> ().holdingBomb = true;
				gameObject.transform.parent.GetChild (0).gameObject.SetActive (false);
			}
		}
		startTimer = false;
		currenTime = 0.0f;
	}
}