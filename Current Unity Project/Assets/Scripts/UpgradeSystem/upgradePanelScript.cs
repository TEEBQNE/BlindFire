using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradePanelScript : MonoBehaviour {

	public static upgradePanelScript upgradePanel;

	public string currentTarget = "close";

	public Image turretImage;
	public Image upgrade1;
	public Image upgrade2;
	public Image upgrade1Lvl1;
	public Image upgrade1Lvl2;
	public Image upgrade1Lvl3;
	public Image upgrade2Lvl1;
	public Image upgrade2Lvl2;
	public Image upgrade2Lvl3;
	public Image upgradeInfo;



	public Button firstButton;
	public Button lastButton;
	public Button stongButton;
	public Button closeButton;
	public Button sellButton;
	public Button upgrade1Button;
	public Button upgrade2Button;


	public Text turretName;
	public Text killAmount;
	public Text upgrade1CanBuy;
	public Text upgrade1Price;
	public Text upgrade2CanBuy;
	public Text upgrade2Price;
	public Text upgradeInfoName;
	public Text upgradeInfoPrice;
	public Text upgradeInfoDescrption;


	public bool firstPressed = false;
	public bool lastPressed = false;
	public bool strongPressed = false;
	public bool closePressed = false;
	public bool sellPressed = false;
	public bool upgrade1Pressed = false;
	public bool upgrade2Pressed = false;

	public Button currentButtonTarget;

	public bool upgrade1Hover = false;
	public bool upgrade2Hover = false;


	public GameObject currentSelectedTurret = null;

	public bool showShop = false;

	// Use this for initialization
	void Start () {
		upgradePanel = this;
		currentButtonTarget = closeButton;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentSelectedTurret != null)
		{
			turretName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().turretName;
			turretImage.GetComponent<Image> ().sprite = currentSelectedTurret.GetComponent<turretUpgradeScript> ().turretImage;
			sellButton.transform.Find ("sellText").GetComponent<Text> ().text = "Sell For: " + "$ " + Mathf.Round(.8f*currentSelectedTurret.GetComponent<turretUpgradeScript> ().totalTurretCost).ToString();

			GameObject localPlayer22 = GameObject.Find("localPlayer2");
			if (localPlayer22 != null) {
				localPlayer22.GetComponent<networkPlayerScript> ().hasKilled = true;
				// UPDATE KILLS AMOUNT
				killAmount.GetComponent<Text> ().text = "Alien Kills " + localPlayer22.GetComponent<networkPlayerScript> ().turretKills.ToString ();

			}
			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentTargeting.CompareTo ("first") == 0) {
				firstButton.GetComponent<upgradeButtonScript> ().activeButton = firstButton;
				lastButton.GetComponent<upgradeButtonScript> ().activeButton = firstButton;
				stongButton.GetComponent<upgradeButtonScript> ().activeButton = firstButton;
				closeButton.GetComponent<upgradeButtonScript> ().activeButton = firstButton;

				currentButtonTarget = firstButton;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentTargeting.CompareTo ("last") == 0) {
				firstButton.GetComponent<upgradeButtonScript> ().activeButton = lastButton;
				lastButton.GetComponent<upgradeButtonScript> ().activeButton = lastButton;
				stongButton.GetComponent<upgradeButtonScript> ().activeButton = lastButton;
				closeButton.GetComponent<upgradeButtonScript> ().activeButton = lastButton;

				currentButtonTarget = lastButton;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentTargeting.CompareTo ("strong") == 0) {
				firstButton.GetComponent<upgradeButtonScript> ().activeButton = stongButton;
				lastButton.GetComponent<upgradeButtonScript> ().activeButton = stongButton;
				stongButton.GetComponent<upgradeButtonScript> ().activeButton = stongButton;
				closeButton.GetComponent<upgradeButtonScript> ().activeButton = stongButton;

				currentButtonTarget = closeButton;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentTargeting.CompareTo ("close") == 0) {
				firstButton.GetComponent<upgradeButtonScript> ().activeButton = closeButton;
				lastButton.GetComponent<upgradeButtonScript> ().activeButton = closeButton;
				stongButton.GetComponent<upgradeButtonScript> ().activeButton = closeButton;
				closeButton.GetComponent<upgradeButtonScript> ().activeButton = closeButton;

				currentButtonTarget = stongButton;
			}
			// SETTING COLOR OF UPGRADE BUTTONS IF MONEY IS NOT ENOUGH
			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 0) {
				if (GameManager.gameManager.GetComponent<GameManager> ().Resource < currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Price) {
					upgrade1CanBuy.GetComponent<Text> ().text = "Cant Buy";
					upgrade1Button.GetComponent<Image> ().color = new Color (155f/255f, 0f, 0f, upgrade1Button.GetComponent<Image> ().color.a);
				} else {
					upgrade1CanBuy.GetComponent<Text> ().text = "Buy";
					upgrade1Button.GetComponent<Image> ().color = new Color (94f/255f, 41/255f, 117/255f, upgrade1Button.GetComponent<Image> ().color.a);
				}
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Name;
				upgradeInfoPrice.GetComponent<Text> ().text =  "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 1) {
				if (GameManager.gameManager.GetComponent<GameManager> ().Resource < currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Price) {
					upgrade1CanBuy.GetComponent<Text> ().text = "Cant Buy";
					upgrade1Button.GetComponent<Image> ().color = new Color (155f/255f, 0f, 0f, upgrade1Button.GetComponent<Image> ().color.a);
				} else {
					upgrade1CanBuy.GetComponent<Text> ().text = "Buy";
					upgrade1Button.GetComponent<Image> ().color = new Color (94f/255f, 41/255f, 117/255f, upgrade1Button.GetComponent<Image> ().color.a);
				}
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Name;
				upgradeInfoPrice.GetComponent<Text> ().text =  "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 2) {
				if (GameManager.gameManager.GetComponent<GameManager> ().Resource < currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Price) {
					upgrade1CanBuy.GetComponent<Text> ().text = "Cant Buy";
					upgrade1Button.GetComponent<Image> ().color = new Color (155f/255f, 0f, 0f, upgrade1Button.GetComponent<Image> ().color.a);
				} else {
					upgrade1CanBuy.GetComponent<Text> ().text = "Buy";
					upgrade1Button.GetComponent<Image> ().color = new Color (94f/255f, 41/255f, 117/255f, upgrade1Button.GetComponent<Image> ().color.a);
				}
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Name;
				upgradeInfoPrice.GetComponent<Text> ().text =  "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 3) {
				upgradeInfoName.GetComponent<Text> ().text = "";
				upgradeInfoPrice.GetComponent<Text> ().text = "MAXED OUT";
				upgradeInfoDescrption.GetComponent<Text> ().text = "";
				upgrade1Button.GetComponent<Image> ().color = new Color (94f/255f, 41/255f, 117/255f, upgrade1Button.GetComponent<Image> ().color.a);
			}

			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 0) {
				if (GameManager.gameManager.GetComponent<GameManager> ().Resource < currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Price) {
					upgrade2CanBuy.GetComponent<Text> ().text = "Cant Buy";
					upgrade2Button.GetComponent<Image> ().color = new Color (155f/255f, 0f, 0f, upgrade1Button.GetComponent<Image> ().color.a);
				} else {
					upgrade2CanBuy.GetComponent<Text> ().text = "Buy";
					upgrade2Button.GetComponent<Image> ().color = new Color (94f/255f, 41/255f, 117/255f, upgrade1Button.GetComponent<Image> ().color.a);
				}
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Name;
				upgradeInfoPrice.GetComponent<Text> ().text =  "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 1) {
				if (GameManager.gameManager.GetComponent<GameManager> ().Resource < currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Price) {
					upgrade2CanBuy.GetComponent<Text> ().text = "Cant Buy";
					upgrade2Button.GetComponent<Image> ().color = new Color (155f/255f, 0f, 0f, upgrade1Button.GetComponent<Image> ().color.a);
				} else {
					upgrade2CanBuy.GetComponent<Text> ().text = "Buy";
					upgrade2Button.GetComponent<Image> ().color = new Color (94f/255f, 41/255f, 117/255f, upgrade1Button.GetComponent<Image> ().color.a);
				}
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Name;
				upgradeInfoPrice.GetComponent<Text> ().text =  "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 2) {
				if (GameManager.gameManager.GetComponent<GameManager> ().Resource < currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Price) {
					upgrade2CanBuy.GetComponent<Text> ().text = "Cant Buy";
					upgrade2Button.GetComponent<Image> ().color = new Color (155f/255f, 0f, 0f, upgrade1Button.GetComponent<Image> ().color.a);
				} else {
					upgrade2CanBuy.GetComponent<Text> ().text = "Buy";
					upgrade2Button.GetComponent<Image> ().color = new Color (94f/255f, 41/255f, 117/255f, upgrade1Button.GetComponent<Image> ().color.a);
				}
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Name;
				upgradeInfoPrice.GetComponent<Text> ().text = "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 3) {
				upgradeInfoName.GetComponent<Text> ().text = "";
				upgradeInfoPrice.GetComponent<Text> ().text = "MAXED OUT";
				upgradeInfoDescrption.GetComponent<Text> ().text = "";
				upgrade2Button.GetComponent<Image> ().color = new Color (94f/255f, 41/255f, 117/255f, upgrade1Button.GetComponent<Image> ().color.a);
			}

			
			// INITIAL PRICE SET UP FOR UPGRADES
			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 0) {
				//upgrade1Lvl1.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade1Lvl2.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);
				upgrade1Lvl1.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);
				upgrade1Lvl3.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);
				upgrade1Price.GetComponent<Text>().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Price.ToString ();
			}
			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 1) {
				upgrade1Price.GetComponent<Text>().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Price.ToString ();
				//upgrade1Lvl2.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade1Lvl2.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);
				upgrade1Lvl1.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade1Lvl3.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);
			}
			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 2) {
				upgrade1Price.GetComponent<Text>().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Price.ToString ();
				upgrade1Lvl2.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade1Lvl1.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade1Lvl3.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);
			}
			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 3) {
				upgrade1Price.GetComponent<Text>().text = "";
				upgrade1CanBuy.GetComponent<Text> ().text = "Maxed Out";
				upgrade1Lvl2.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade1Lvl1.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade1Lvl3.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
			}

			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 0) {
				upgrade2Price.GetComponent<Text>().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Price.ToString ();
				upgrade2Lvl2.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);
				upgrade2Lvl1.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);
				upgrade2Lvl3.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);

			}
			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 1) {
				upgrade2Price.GetComponent<Text>().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Price.ToString ();
				//upgrade2Lvl2.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade2Lvl1.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade2Lvl2.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);
				upgrade2Lvl3.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);
			}
			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 2) {
				upgrade2Price.GetComponent<Text>().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Price.ToString ();
				upgrade2Lvl2.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade2Lvl1.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade2Lvl3.GetComponent<Image> ().color = new Color(169f/255f, 164f/255f, 164f/255f, 216f/255f);
				//upgrade1Lvl3.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
			}

			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 3) {
				upgrade2Price.GetComponent<Text>().text = "";
				upgrade2CanBuy.GetComponent<Text> ().text = "Maxed Out";
				upgrade2Lvl2.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade2Lvl1.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
				upgrade2Lvl3.GetComponent<Image> ().color = new Color (188f / 255f, 0f, 1f, 1f);
			}
				
		}
		GameObject localPlayer2;
		localPlayer2 = GameObject.Find ("localPlayer2");
		if (firstPressed) {
			firstPressed = false;
			firstButton.GetComponent<upgradeButtonScript> ().activeButton = firstButton;
			lastButton.GetComponent<upgradeButtonScript> ().activeButton = firstButton;
			stongButton.GetComponent<upgradeButtonScript> ().activeButton = firstButton;
			closeButton.GetComponent<upgradeButtonScript> ().activeButton = firstButton;
			currentSelectedTurret.GetComponent<ShootScript> ().target = "first";
			localPlayer2.GetComponent<networkPlayerScript>().currentTargeting = "first";
			localPlayer2.GetComponent<networkPlayerScript> ().updateTargeting = true;
			currentButtonTarget = firstButton;
			//currentButtonTarget.GetComponent<upgradeButtonScript> ().isActiveTarget = true;

		}
		if (lastPressed) {
			lastPressed = false;
			firstButton.GetComponent<upgradeButtonScript> ().activeButton = lastButton;
			lastButton.GetComponent<upgradeButtonScript> ().activeButton = lastButton;
			stongButton.GetComponent<upgradeButtonScript> ().activeButton = lastButton;
			closeButton.GetComponent<upgradeButtonScript> ().activeButton = lastButton;

			currentSelectedTurret.GetComponent<ShootScript> ().target = "last";
			localPlayer2.GetComponent<networkPlayerScript>().currentTargeting = "last";
			localPlayer2.GetComponent<networkPlayerScript> ().updateTargeting = true;

			currentButtonTarget = lastButton;
			//currentButtonTarget.GetComponent<upgradeButtonScript> ().isActiveTarget = true;

		}
		if (strongPressed) {
			strongPressed = false;
			stongButton.GetComponent<upgradeButtonScript> ().activeButton = stongButton;
			lastButton.GetComponent<upgradeButtonScript> ().activeButton = stongButton;
			stongButton.GetComponent<upgradeButtonScript> ().activeButton = stongButton;
			closeButton.GetComponent<upgradeButtonScript> ().activeButton = stongButton;
			currentSelectedTurret.GetComponent<ShootScript> ().target = "strong";
			localPlayer2.GetComponent<networkPlayerScript>().currentTargeting = "strong";
			localPlayer2.GetComponent<networkPlayerScript> ().updateTargeting = true;
			currentButtonTarget = stongButton;
			//currentButtonTarget.GetComponent<upgradeButtonScript> ().isActiveTarget = true;

		}
		if (closePressed) {
			closePressed = false;
			firstButton.GetComponent<upgradeButtonScript> ().activeButton = closeButton;
			lastButton.GetComponent<upgradeButtonScript> ().activeButton = closeButton;
			stongButton.GetComponent<upgradeButtonScript> ().activeButton = closeButton;
			closeButton.GetComponent<upgradeButtonScript> ().activeButton = closeButton;
			currentSelectedTurret.GetComponent<ShootScript> ().target = "close";
			localPlayer2.GetComponent<networkPlayerScript>().currentTargeting = "close";
			localPlayer2.GetComponent<networkPlayerScript> ().updateTargeting = true;
			currentButtonTarget = closeButton;
			//currentButtonTarget.GetComponent<upgradeButtonScript> ().isActiveTarget = true;
		}
		if (sellPressed) {
			GameManager.gameManager.GetComponent<GameManager> ().Resource += Mathf.Round (.8f * currentSelectedTurret.GetComponent<turretUpgradeScript> ().totalTurretCost);
			//currentSelectedTurret.GetComponent<ShootScript> ().enabled = false;
			//currentSelectedTurret.GetComponent<turretUpgradeScript> ().enabled = false;
			//currentSelectedTurret.GetComponent<turretShopSpawn> ().enabled = false;

			localPlayer2.GetComponent<networkPlayerScript> ().isSold = true;
			//Destroy (currentSelectedTurret);
			currentButtonTarget = null;
			sellButton.GetComponent<Outline> ().enabled = false;
			showShop = false;
			sellPressed = false;
		}

		// UPDATES TURRET/RESOURCES IF UPGRADE IS PRESSED
		if (upgrade1Pressed) {
			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 0 && currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Price < GameManager.gameManager.GetComponent<GameManager>().Resource) {
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1++;
					upgrade1Price.GetComponent<Text>().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Price.ToString ();
					GameManager.gameManager.GetComponent<GameManager>().Resource -= currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Price;
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().totalTurretCost += currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Price;
					//currentSelectedTurret.GetComponent<ShootScript> ().damage = currentSelectedTurret.GetComponent<turretUpgradeScript> ().damage1;
					localPlayer2.GetComponent<networkPlayerScript>().upgrade1Level = 1;
					localPlayer2.GetComponent<networkPlayerScript> ().currentDamage = currentSelectedTurret.GetComponent<turretUpgradeScript> ().damage1;
					localPlayer2.GetComponent<networkPlayerScript>().upgrade1Bought = true;
					localPlayer2.GetComponent<networkPlayerScript> ().upgrade1Price = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Price;
				} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 1 && currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Price < GameManager.gameManager.GetComponent<GameManager>().Resource) {
					upgrade1Price.GetComponent<Text>().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Price.ToString ();
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().totalTurretCost += currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Price;
					//currentSelectedTurret.GetComponent<ShootScript> ().damage = currentSelectedTurret.GetComponent<turretUpgradeScript> ().damage2;
					localPlayer2.GetComponent<networkPlayerScript> ().currentDamage = currentSelectedTurret.GetComponent<turretUpgradeScript> ().damage2;
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1++;
					localPlayer2.GetComponent<networkPlayerScript>().upgrade1Level = 2;
					localPlayer2.GetComponent<networkPlayerScript>().upgrade1Bought = true;
					localPlayer2.GetComponent<networkPlayerScript> ().upgrade1Price = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Price;
					GameManager.gameManager.GetComponent<GameManager>().Resource -= currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Price;
				} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 2 && currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Price < GameManager.gameManager.GetComponent<GameManager>().Resource) {
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().totalTurretCost += currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Price;
					//currentSelectedTurret.GetComponent<ShootScript> ().damage = currentSelectedTurret.GetComponent<turretUpgradeScript> ().damage3;
					localPlayer2.GetComponent<networkPlayerScript> ().currentDamage = currentSelectedTurret.GetComponent<turretUpgradeScript> ().damage3;
					upgrade1Price.GetComponent<Text> ().text = "";
					upgrade1CanBuy.GetComponent<Text> ().text = "MAXED OUT";
					localPlayer2.GetComponent<networkPlayerScript>().upgrade1Level = 3;
					localPlayer2.GetComponent<networkPlayerScript>().upgrade1Bought = true;
					localPlayer2.GetComponent<networkPlayerScript> ().upgrade1Price = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Price;
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1++;
					GameManager.gameManager.GetComponent<GameManager>().Resource -= currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Price;
			}
			upgrade1Pressed = false;
		}
		if (upgrade2Pressed) {
				if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 0 && currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Price < GameManager.gameManager.GetComponent<GameManager>().Resource) {
					upgrade2Price.GetComponent<Text>().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Price.ToString ();
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().totalTurretCost += currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Price;
					//currentSelectedTurret.transform.Find("visionCollider").GetComponent<CircleCollider2D> ().radius = currentSelectedTurret.GetComponent<turretUpgradeScript> ().range1;
					localPlayer2.GetComponent<networkPlayerScript> ().currentRadius = currentSelectedTurret.GetComponent<turretUpgradeScript> ().range1;
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2++;
					localPlayer2.GetComponent<networkPlayerScript>().upgrade2Level = 1;
					localPlayer2.GetComponent<networkPlayerScript>().upgrade2Bought = true;
					localPlayer2.GetComponent<networkPlayerScript> ().upgrade2Price = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Price;
					GameManager.gameManager.GetComponent<GameManager>().Resource -= currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Price;
				} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 1 && currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Price < GameManager.gameManager.GetComponent<GameManager>().Resource) {
					upgrade2Price.GetComponent<Text>().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Price.ToString ();
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().totalTurretCost += currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Price;
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2++;
					GameManager.gameManager.GetComponent<GameManager>().Resource -= currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Price;
					//currentSelectedTurret.transform.Find("visionCollider").GetComponent<CircleCollider2D> ().radius = currentSelectedTurret.GetComponent<turretUpgradeScript> ().range2;
					localPlayer2.GetComponent<networkPlayerScript> ().currentRadius = currentSelectedTurret.GetComponent<turretUpgradeScript> ().range2;
					localPlayer2.GetComponent<networkPlayerScript>().upgrade2Level = 2;
					localPlayer2.GetComponent<networkPlayerScript>().upgrade2Bought = true;
					localPlayer2.GetComponent<networkPlayerScript> ().upgrade2Price = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Price;
				} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 2 && currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Price < GameManager.gameManager.GetComponent<GameManager>().Resource) {
					upgrade2Price.GetComponent<Text> ().text = "";
					upgrade2CanBuy.GetComponent<Text> ().text = "MAXED OUT";
					localPlayer2.GetComponent<networkPlayerScript>().upgrade2Level = 3;
					localPlayer2.GetComponent<networkPlayerScript> ().currentRadius = currentSelectedTurret.GetComponent<turretUpgradeScript> ().range3;
					localPlayer2.GetComponent<networkPlayerScript>().upgrade2Bought = true;
					localPlayer2.GetComponent<networkPlayerScript> ().upgrade2Price = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Price;
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().totalTurretCost += currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Price;
					//currentSelectedTurret.transform.Find("visionCollider").GetComponent<CircleCollider2D> ().radius = currentSelectedTurret.GetComponent<turretUpgradeScript> ().range3;
					currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2++;
					GameManager.gameManager.GetComponent<GameManager>().Resource -= currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Price;
				}
			upgrade2Pressed = false;
		}
			

		// UPDATING UPGRADE INFO
		if (upgrade1Hover) {
			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 0) {
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Name;
				upgradeInfoPrice.GetComponent<Text> ().text =  "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl1Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 1) {
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Name;
				upgradeInfoPrice.GetComponent<Text> ().text =  "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl2Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 2) {
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Name;
				upgradeInfoPrice.GetComponent<Text> ().text =  "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade1Lvl3Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 3) {
				upgradeInfoName.GetComponent<Text> ().text = "";
				upgradeInfoPrice.GetComponent<Text> ().text = "";
				upgradeInfoDescrption.GetComponent<Text> ().text = "";
			}
			upgradeInfo.gameObject.SetActive (true);
			//upgrade1Hover = false;
		}
			
		if (upgrade2Hover) {
			if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 0) {
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Name;
				upgradeInfoPrice.GetComponent<Text> ().text =  "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl1Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 1) {
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Name;
				upgradeInfoPrice.GetComponent<Text> ().text =  "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl2Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 2) {
				upgradeInfoName.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Name;
				upgradeInfoPrice.GetComponent<Text> ().text = "$ " + currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Price.ToString();
				upgradeInfoDescrption.GetComponent<Text> ().text = currentSelectedTurret.GetComponent<turretUpgradeScript> ().upgrade2Lvl3Description;
			} else if (currentSelectedTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 == 3) {
				upgradeInfoName.GetComponent<Text> ().text = "";
				upgradeInfoPrice.GetComponent<Text> ().text = "";
				upgradeInfoDescrption.GetComponent<Text> ().text = "";
			}
			upgradeInfo.gameObject.SetActive (true);
		}

		if (!upgrade2Hover && !upgrade1Hover) {
			upgradeInfoName.GetComponent<Text> ().text = "";
			upgradeInfoPrice.GetComponent<Text> ().text = "";
			upgradeInfoDescrption.GetComponent<Text> ().text = "";
			upgradeInfo.gameObject.SetActive (false);
		}

		if (!showShop) {
			currentSelectedTurret = null;
			currentButtonTarget = null;
			upgrade1Button.gameObject.SetActive (false);
			upgrade2Button.gameObject.SetActive (false);
			firstButton.gameObject.SetActive (false);
			lastButton.gameObject.SetActive (false);
			stongButton.gameObject.SetActive (false);
			closeButton.gameObject.SetActive (false);
			sellButton.gameObject.SetActive (false);
			upgradeInfo.enabled = false;
			turretImage.enabled = false;
			upgrade1.enabled = false;
			upgrade2.enabled = false;
			upgrade1Lvl1.enabled = false;
			upgrade1Lvl2.enabled = false;
			upgrade1Lvl3.enabled = false;
			upgrade2Lvl1.enabled = false;
			upgrade2Lvl2.enabled = false;
			upgrade2Lvl3.enabled = false;
			sellButton.enabled = false;
			turretName.enabled = false;
			killAmount.enabled = false;
			upgrade1CanBuy.enabled = false;
			upgrade1Price.enabled = false;
			upgrade2CanBuy.enabled = false;
			upgrade2Price.enabled = false;
			upgradeInfoName.enabled = false;
			upgradeInfoPrice.enabled = false;
			upgradeInfoDescrption.enabled = false;
		} else {
			upgrade1Button.gameObject.SetActive (true);
			upgrade2Button.gameObject.SetActive (true);
			upgradeInfo.enabled = true;
			firstButton.gameObject.SetActive (true);
			lastButton.gameObject.SetActive (true);
			stongButton.gameObject.SetActive (true);
			closeButton.gameObject.SetActive (true);
			sellButton.gameObject.SetActive (true);
			upgradeInfo.enabled = true;
			turretImage.enabled = true;
			upgrade1.enabled = true;
			upgrade2.enabled = true;
			upgrade1Lvl1.enabled = true;
			upgrade1Lvl2.enabled = true;
			upgrade1Lvl3.enabled = true;
			upgrade2Lvl1.enabled = true;
			upgrade2Lvl2.enabled = true;
			upgrade2Lvl3.enabled = true;
			sellButton.enabled = true;
			turretName.enabled = true;
			killAmount.enabled = true;
			upgrade1CanBuy.enabled = true;
			upgrade1Price.enabled = true;
			upgrade2CanBuy.enabled = true;
			upgrade2Price.enabled = true;
			upgradeInfoName.enabled = true;
			upgradeInfoPrice.enabled = true;
			upgradeInfoDescrption.enabled = true;
		}
	}
}