using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shopButtonScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {

	GameObject theCanvas;
	GameObject UIHolder;
	GameObject activeMap;


	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Outline> ().enabled = false;
		theCanvas = GameObject.Find ("Canvas");
		UIHolder = GameObject.Find ("UIHolder");
		activeMap = GameObject.Find ("activeEnemy");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void openShop()
	{
		if (!GameManager.gameManager.GetComponent<GameManager> ().holdingBomb) {
			if (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject != null && GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretShopSpawn> ().isClicked && !GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretShopSpawn> ().followMouse) {
				gameObject.GetComponent<Outline> ().enabled = false;
				activeMap.transform.Find ("enemyPath").gameObject.SetActive (false);

				GameObject[] turrets;
				turrets = GameObject.FindGameObjectsWithTag ("turret");

				foreach (GameObject theTurrets in turrets) {
					theTurrets.SetActive (false);
				}
				GameManager.gameManager.GetComponent<GameManager> ().shopOpen = true;
				UIHolder.transform.Find ("shopPanel").gameObject.SetActive (true);
				UIHolder.transform.Find ("shopBackground").gameObject.SetActive (true);
				theCanvas.transform.Find ("PlayButton").gameObject.SetActive (false);
				theCanvas.transform.Find ("shopButton").gameObject.SetActive (false);
			} else if (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject == null) {
				gameObject.GetComponent<Outline> ().enabled = false;
				activeMap.transform.Find ("enemyPath").gameObject.SetActive (false);

				GameObject[] turrets;
				turrets = GameObject.FindGameObjectsWithTag ("turret");

				foreach (GameObject theTurrets in turrets) {
					theTurrets.SetActive (false);
				}
				GameManager.gameManager.GetComponent<GameManager> ().shopOpen = true;
				UIHolder.transform.Find ("shopBackground").gameObject.SetActive (true);
				UIHolder.transform.Find ("shopPanel").gameObject.SetActive (true);
				theCanvas.transform.Find ("PlayButton").gameObject.SetActive (false);
				theCanvas.transform.Find ("shopButton").gameObject.SetActive (false);
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		gameObject.GetComponent<Outline> ().enabled = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		gameObject.GetComponent<Outline> ().enabled = false;
	}
}
