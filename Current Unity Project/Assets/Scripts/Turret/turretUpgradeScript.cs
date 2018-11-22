using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class turretUpgradeScript : MonoBehaviour {

	public Material outline;
	public Material basic;

	public GameObject localPlayer1;

	//bool shouldUpdateShop = false;

	// basic turret info
	// shop image or alt art
	public Sprite turretImage;

	// turret name
	public string turretName;

	// current amount of enemies turret has killed
	public int killAmount;

	// starting cost of turret
	public int initialTurretCost;

	// compounded cost of initial cost + all upgrades
	public int totalTurretCost;

	// current targeting method (first, last, strong, close)
	public string currentTargeting;

	// upgrade 1 stuff
	// upgrade 1 sprite
	public Sprite upgrade1Image;

	// upgrade 1 current level
	public int currentUpgradeLevel1;

	// upgrade 1 level 1 data
	public int upgrade1Lvl1Price;
	public string upgrade1Lvl1Name;
	public string upgrade1Lvl1Description;
	public int damage1;

	// upgrade 1 level 2 data
	public int upgrade1Lvl2Price;
	public string upgrade1Lvl2Name;
	public string upgrade1Lvl2Description;
	public int damage2;

	// upgrade 1 level 3 data
	public int upgrade1Lvl3Price;
	public string upgrade1Lvl3Name;
	public string upgrade1Lvl3Description;
	public int damage3;

	// upgrade 2 stuff
	// upgrade 2 sprite
	public Sprite upgrade2Image;

	// upgrade 2 current level 
	public int currentUpgradeLevel2;

	// upgrade 2 level 1 data
	public int upgrade2Lvl1Price;
	public string upgrade2Lvl1Name;
	public string upgrade2Lvl1Description;
	public float range1;

	// upgrade 2 level 2 data
	public int upgrade2Lvl2Price;
	public string upgrade2Lvl2Name;
	public string upgrade2Lvl2Description;
	public float range2;

	// upgrade 2 level 3 data
	public int upgrade2Lvl3Price;
	public string upgrade2Lvl3Name;
	public string upgrade2Lvl3Description;
	public float range3;

	// Use this for initialization
	void Start () {
		localPlayer1 = GameObject.Find ("localPlayer1");
	}

	// Update is called once per frame
	void Update () {

	}


	void OnMouseDown()
	{
		if(GameManager.gameManager.GetComponent<GameManager>().following == false && gameObject.GetComponent<turretShopSpawn>().followMouse == false)
		{
			if (upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret != null) {
				GameObject localPlayer2 = GameObject.Find ("localPlayer2");
				localPlayer2.GetComponent<networkPlayerScript> ().toggleViewField = false;
				localPlayer2.GetComponent<networkPlayerScript> ().turretID = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().turretID;
				localPlayer2.GetComponent<networkPlayerScript> ().gameObjectName = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.gameObject.name;
				localPlayer2.GetComponent<networkPlayerScript> ().setViewField = true;
				upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret.transform.Find ("ViewField").gameObject.SetActive (false);
			}
			if (GameManager.gameManager.GetComponent<GameManager> ().following == false) {
				//upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret = this.gameObject;
				//updateShopInfo ();
				//upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = true;
				//shouldUpdateShop = true;
				GameManager.gameManager.GetComponent<GameManager>().activelyPressedObject = this.gameObject;
				Debug.Log (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.name);
				localPlayer1.GetComponent<networkPlayerScript> ().clickedTurret = true;
				localPlayer1.GetComponent<networkPlayerScript> ().toggleClicked = true;
				transform.Find ("ViewField").gameObject.SetActive (true);
			}
		}
	}

	void OnMouseOver()
	{
	if (gameObject.GetComponent<turretShopSpawn>().followMouse == false) {
			//gameObject.transform.parent.GetComponent<SpriteRenderer> ().material = outline;
			gameObject.GetComponent<SpriteRenderer> ().material = outline;
			//return;
		}
	}

	void OnMouseExit()
	{
		if (gameObject.GetComponent<turretShopSpawn>().followMouse == false) {
			//gameObject.transform.parent.GetComponent<SpriteRenderer> ().material = outline;
			gameObject.GetComponent<SpriteRenderer> ().material = basic;
			//return;
		}
	}
}