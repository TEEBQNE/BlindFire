using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class resScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {

	float timeToBuy = 0.15f;
	float currenTime = 0.0f;
	public int resPrice = 250;
	public int percentHeal = 100;

	bool startTimer = false;

	// Use this for initialization
	void Start () {
		gameObject.transform.parent.GetChild (2).transform.Find ("resPrice").GetComponent<Text>().text = "$ " + resPrice.ToString ();
	}

	// Update is called once per frame
	void Update () {
		if (startTimer) {
			currenTime += Time.deltaTime;
		}

		if (GameManager.gameManager.GetComponent<GameManager> ().Resource >= resPrice && GameManager.gameManager.GetComponent<GameManager> ().isDead) {
			gameObject.GetComponent<Image> ().color = Color.white;
			transform.parent.GetChild(1).GetComponent<Image>().color =  Color.white;
		}

		if (GameManager.gameManager.GetComponent<GameManager> ().Resource >= resPrice && !GameManager.gameManager.GetComponent<GameManager> ().isDead) {
			gameObject.GetComponent<Image> ().color = new Color (107f / 255f, 107f / 255f, 107f / 255f, 144f / 255f);
			transform.parent.GetChild(1).GetComponent<Image>().color = new Color (107f / 255f, 107f / 255f, 107f / 255f, 144f / 255f);
		}

		if (GameManager.gameManager.GetComponent<GameManager> ().Resource <= resPrice && !GameManager.gameManager.GetComponent<GameManager> ().isDead) {
			gameObject.GetComponent<Image> ().color = new Color (210f / 255f, 48f / 255f, 48f / 255f, 166f / 255f);
			transform.parent.GetChild(1).GetComponent<Image>().color = new Color (210f / 255f, 48f / 255f, 48f / 255f, 166f / 255f);
		}

		if (GameManager.gameManager.GetComponent<GameManager> ().Resource <= resPrice && GameManager.gameManager.GetComponent<GameManager> ().isDead) {
			gameObject.GetComponent<Image> ().color = new Color (203f / 255f, 33f / 255f, 33f / 255f, 255f / 255f);
			transform.parent.GetChild(1).GetComponent<Image>().color = new Color (203f / 255f, 33f / 255f, 33f / 255f, 255f / 255f);
		}
		// if is alive or not enough money, make it transparent and red
		// else if you have enough money and the player is dead make it clear
		// else if he is alive and you have enough money, just make it clear but not red


		/*if (GameManager.gameManager.GetComponent<GameManager> ().Resource < bombPrice && gameObject.name == "bombButton") {
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
		}*/
	}

	public void resFucntion()
	{
		if (currenTime > timeToBuy) {
		} else {
			if (GameManager.gameManager.GetComponent<GameManager> ().Resource >= resPrice && GameManager.gameManager.GetComponent<GameManager>().isDead) {
				GameManager.gameManager.GetComponent<GameManager> ().Resource -= resPrice;
				GameObject localPlayer2 = GameObject.Find ("localPlayer2");
				localPlayer2.GetComponent<networkPlayerScript> ().resPlayer = true;
				localPlayer2.GetComponent<networkPlayerScript> ().updateRes = true;
				GameManager.gameManager.GetComponent<GameManager> ().isDead = false;
			}
		}
		startTimer = false;
		currenTime = 0.0f;

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		startTimer = true;
		gameObject.GetComponent<Outline> ().enabled = true;
		gameObject.transform.parent.GetChild (2).gameObject.SetActive (true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		gameObject.GetComponent<Outline> ().enabled = false;
		gameObject.transform.parent.GetChild (2).gameObject.SetActive (false);
	}
}