using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using UnityEngine.UI;



public class BuyButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {
	public int turretID;

	bool startTimer = false;
	float currentTimer = 0.0f;
	float timerAmount = 0.15f;

	public GameObject infoScreen;

	GameObject turret;
	GameObject turretInstance = null;
	//float activeTurretCost = 0f;

	bool isOver = false;

	void Start()
	{
		
	}

	void Update()
	{
		if (startTimer) {
			currentTimer += Time.deltaTime;
		}
		/*if (!GameManager.gameManager.GetComponent<GameManager> ().following) {
			turretInstance = null;
			activeTurretCost = 0;
		}*/

		/*if (Input.GetKey (KeyCode.Escape) && turretInstance != null) {
			Destroy (turretInstance);
			GameManager.gameManager.GetComponent<GameManager> ().following = false;
			GameManager.gameManager.GetComponent<GameManager> ().Resource += activeTurretCost;
			activeTurretCost = 0;
			turretInstance = null;
		}*/

		// removes outline around button when you do not have enough money for purchase
		if (isOver) {
			for (int x = 0; x < TurretShop.turretShop.turretList.Count; x++) {
				// FOUND CORRECT TURRET BASED ON ID
				if (TurretShop.turretShop.turretList [x].turretID == turretID) {
					if (TurretShop.turretShop.turretList [x].unlocked && TurretShop.turretShop.turretList [x].turretPrice > GameManager.gameManager.GetComponent<GameManager> ().Resource) {
						gameObject.transform.parent.transform.Find ("itemImage").GetComponent<Outline> ().enabled = false;
						infoScreen.SetActive (true);
					}
				}
			}
		}
	}

	// ON CLICK EVENT
	public void BuyTurret()
	{
		if (currentTimer > timerAmount) {

		} else {
			// NO WEAPON ID SET ERROR
			if (turretID == 0) {
				Debug.Log ("NO WEAPON ID FOUND");
				return;
			}

			/*if (GameManager.gameManager.GetComponent<GameManager> ().following) {
			// already holding an item
			return;
		}*/
			
			for (int x = 0; x < TurretShop.turretShop.turretList.Count; x++) {
				// FOUND CORRECT TURRET BASED ON ID
				if (TurretShop.turretShop.turretList [x].turretID == turretID) {
					// IF IT IS UNLOCKED
					if (TurretShop.turretShop.turretList [x].unlocked) {
						// IF YOU HAVE ENOUGH MONEY TO BUY
						if (GameManager.gameManager.getMoney (TurretShop.turretShop.turretList [x].turretPrice)) {
							// BUY IT
							// if you are holding a gameobject and you just  bougt the same object
								/*if (GameManager.gameManager.GetComponent<GameManager> ().following && GameManager.gameManager.GetComponent<GameManager> ().turret.GetComponent<turretUpgradeScript> ().turretName.CompareTo (TurretShop.turretShop.turretList [x].turretName) == 0) {
									//Debug.Log ("SAME TURRET");
									GameManager.gameManager.GetComponent<GameManager> ().Resource += GameManager.gameManager.GetComponent<GameManager> ().turret.GetComponent<turretUpgradeScript> ().initialTurretCost;
									Destroy (GameManager.gameManager.GetComponent<GameManager> ().turret);
									GameManager.gameManager.GetComponent<GameManager> ().following = false;
									GameManager.gameManager.GetComponent<GameManager> ().turret = null;
									// already holding an item
									// Destroy the old object and return the resources to the player
									// then set following to false
									// do this by storing active held gameobject in gamemanager
									// and storing the turrets price in its object
									// so access the public object in gamemanager to first return money
									// then destroy it
								} else if (GameManager.gameManager.GetComponent<GameManager> ().following && !(GameManager.gameManager.GetComponent<GameManager> ().turret.GetComponent<turretUpgradeScript> ().turretName.CompareTo (TurretShop.turretShop.turretList [x].turretName) == 0)) {
									GameManager.gameManager.GetComponent<GameManager> ().Resource += GameManager.gameManager.GetComponent<GameManager> ().turret.GetComponent<turretUpgradeScript> ().initialTurretCost;
									Destroy (GameManager.gameManager.GetComponent<GameManager> ().turret);
									GameManager.gameManager.GetComponent<GameManager> ().following = false;
									GameManager.gameManager.GetComponent<GameManager> ().turret = null;
									//Debug.Log ("REPLACE TURRET");
									// Load in the proper serialized string object here
									// Add a string turret name to itemHolder and Turret 
									// then assign in in turretShop and the editor by using the prefabs
									turret = Resources.Load ("Basic Turret") as GameObject;
									turretInstance = Instantiate (turret, transform.position, turret.transform.rotation);
									GameManager.gameManager.GetComponent<GameManager> ().turret = turretInstance;
									GameManager.gameManager.GetComponent<GameManager> ().following = true;
									// SUBTRACT TURRET AMOUNT FROM RESOURCES
									GameManager.gameManager.Resource -= TurretShop.turretShop.turretList [x].turretPrice;
									activeTurretCost = TurretShop.turretShop.turretList [x].turretPrice;
								} else*/
							
									//Debug.Log ("NEW TURRET");
									//turret = Resources.Load ("basicTurret") as GameObject;
									//turretInstance = Instantiate (turret, transform.position, turret.transform.rotation);
									GameObject localPlayer = GameObject.Find ("localPlayer2");
									if (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject != null) {
										localPlayer.GetComponent<networkPlayerScript> ().toggleViewField = false;
										localPlayer.GetComponent<networkPlayerScript> ().turretID = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().turretID;
										localPlayer.GetComponent<networkPlayerScript> ().gameObjectName = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.gameObject.name;
										localPlayer.GetComponent<networkPlayerScript> ().setViewField = true;
									}
									localPlayer.GetComponent<networkPlayerScript> ().nameOfTurretToSpawn = TurretShop.turretShop.turretList [x].turretName;
									localPlayer.GetComponent<networkPlayerScript> ().shouldSpawnTurret = true;
									transform.parent.transform.parent.transform.Find ("itemInfo").gameObject.SetActive (false);
									// create a sound manager for sounds
									//gameObject.GetComponent<AudioSource> ().Play ();
									GameManager.gameManager.GetComponent<GameManager> ().turret = turretInstance;
									//GameManager.gameManager.GetComponent<GameManager> ().following = true;
									// SUBTRACT TURRET AMOUNT FROM RESOURCES
									GameManager.gameManager.Resource -= TurretShop.turretShop.turretList [x].turretPrice;
									//activeTurretCost = TurretShop.turretShop.turretList [x].turretPrice;
						} else {
							// NOT ENOUGH MONEY
						}
					} else {
						// NOT UNLOCKED
					}
				} else {
					// ERROR NO ID FOUND
				}
				TurretShop.turretShop.UpdateSprite (turretID);
			}
		}
		currentTimer = 0.0f;
		startTimer = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		startTimer = true;
		if (turretID == 0) {
			Debug.Log ("NO WEAPON ID FOUND");
			return;
		}
			
		for (int x = 0; x < TurretShop.turretShop.turretList.Count; x++) {
			// FOUND CORRECT TURRET BASED ON ID
			if (TurretShop.turretShop.turretList [x].turretID == turretID) {
				if(TurretShop.turretShop.turretList [x].unlocked /*&& TurretShop.turretShop.turretList [x].turretPrice < GameManager.gameManager.GetComponent<GameManager>().Resource*/)
				{
					gameObject.transform.parent.transform.Find ("itemImage").GetComponent<Outline> ().enabled = true;
					infoScreen.SetActive (true);
					isOver = true;
				}
			}
		}
	}
		
	public void OnPointerExit(PointerEventData eventData)
	{
		if (turretID == 0) {
			Debug.Log ("NO WEAPON ID FOUND");
			return;
		}

		for (int x = 0; x < TurretShop.turretShop.turretList.Count; x++) {
			// FOUND CORRECT TURRET BASED ON ID
			if (TurretShop.turretShop.turretList [x].turretID == turretID) {
				if(TurretShop.turretShop.turretList [x].unlocked)
				{
					gameObject.transform.parent.transform.Find ("itemImage").GetComponent<Outline> ().enabled = false;
					infoScreen.SetActive (false);
					isOver = false;
				}
			}
		}
	}
}