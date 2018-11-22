using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class networkPlayerScript : NetworkBehaviour {

	public bool isDead = false;
	public bool updateDeath = false;

	public bool resPlayer = false;
	public bool updateRes = false;

	public float xCoord = 0f;
	public float yCoord = 0f;
	public bool placeBomb = false;

	public int player2HealAmount = 0;
	public bool increasePlayer2Health = false;

	public bool fullHealth = true;
	public bool updatePlayer2Health = false;

	public float targetXPos = 0f;
	public float targetYPos = 0f;
	public int theTurretID = 0;
	public bool changeTargetPos = false;

	public bool changeTurretDirection = false;
	public int turretToChange = 0;
	public float zAngle = 0f;

	public bool waveOccuring = false;
	public bool toggleWave = false;
	public int theWave = 0;

	public string tileBought = "";
	public bool boughtATile = false;

	public bool tryToStartRound = false;

	// setting the turret to follow position in both scenes
	public bool pickedUp = false;
	public string gameObjectName = "";
	public int turretID = 0;

	public float xPercent = 0f;
	public float yPercent = 0f;

	public bool toggleViewField = false;
	public bool setViewField = false;

	public bool newTurretFollow = false;

	// test to draw circles
	public bool spawnCircle = false;


	// set game win state
	public bool winGame;

	// update health
	public int healthChange = 0;
	public bool updateHealth = false;

	// set game over state
	public bool gameOver = false;

	// add resources to shop
	public int resourcesAdd = 0;
	public bool updateResources = false;

	// placing turret
	public bool shouldSpawnTurret = false;
	public string nameOfTurretToSpawn = "";

	public bool toggleClicked = false;
	public bool clickedTurret = false;

	// tell the shop if the object has been placed
	public bool shouldSetFollowing = false;
	public bool toSetFollowing = false;

	// tell the object how much it is worth / shop
	public bool updateSellPrice = false;
	public int sellPrice = 0;

	// tell the turret/shop what the turret is targeting method
	public bool updateTargeting = false;
	public string currentTargeting = "";

	// if the turret is sold delete it in scene and return resources to player
	public bool isSold = false;

	public string turretResourceName = "";

	// upgrade 1 update
	public bool upgrade1Bought = false;
	public int upgrade1Level = 0;
	public int upgrade1Price = 0;
	public int currentDamage = 0;

	// upgrade 2 update
	public bool upgrade2Bought = false;
	public int upgrade2Level = 0;
	public int upgrade2Price = 0;
	public float currentRadius = 0;

	public int totalSpent;

	public bool hasKilled = false;
	public int turretKills;

	// turret shop image
	public string turretImage = "";

	// turret name
	public string turretName = "";

	// turret price
	public int turretPrice = 0;

	// unique turret name by instance
	public string turretInstanceName = "";

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			return;
		}
		CmdSpawnMyUnit ();
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
		CmdSetName (numberInQueue);

		if (SceneManager.GetActiveScene ().name == "GameScene") {
			if (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject != null && GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretShopSpawn> ().isClicked) {
				upgrade1Level = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1;
				upgrade2Level = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2;
				sellPrice = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().totalTurretCost;
				currentTargeting = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().currentTargeting;
				totalSpent = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().totalTurretCost;
				turretKills = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().kills;

				// send a command here -- all it does is send the same information to the corresponding turret in the other scene
				// 

				CmdToggleClicked (true, turretResourceName, upgrade1Level, upgrade2Level, sellPrice, currentTargeting, totalSpent, turretKills);
			}
		}

		if (updateDeath && numberInQueue == 1) {
			CmdUpdateDeath (isDead);
			updateDeath = false;
		}

		if (updateRes && numberInQueue == 2) {
			CmdUpdateRes (resPlayer);
			updateRes = false;
		}

		if (placeBomb && numberInQueue == 2) {
			CmdPlaceBomb (xCoord, yCoord);
			placeBomb = false;
		}

		if (increasePlayer2Health && numberInQueue == 2) {
			CmdIncreasePlayer2Health (player2HealAmount);
			increasePlayer2Health = false;
		}

		if (updatePlayer2Health && numberInQueue == 1) {
			CmdUpdatePlayer2Health (fullHealth);
			updatePlayer2Health = false;
		}

		if (changeTargetPos && numberInQueue == 2) {
			CmdChangeTargetPos (targetXPos, targetYPos, theTurretID);
		}

		if (changeTurretDirection && numberInQueue == 2) {
			changeTurretDirection = false;
			CmdUpdateTurretDirection (turretToChange, zAngle);
		}

		if (toggleWave && numberInQueue == 1) {
			toggleWave = false;
			CmdUpdateWave (waveOccuring, theWave);
		}

		if (boughtATile && numberInQueue == 2) {
			boughtATile = false;
			CmdUpdateTiles (tileBought);
		}

		if (tryToStartRound && numberInQueue == 2) {
			tryToStartRound = false;
			CmdTryToStartRound ();
		}

		if (hasKilled && numberInQueue == 2) {
			hasKilled = false;
			CmdUpdateKills ();
			CmdKillCount (turretKills);
		}

		if (setViewField && numberInQueue == 2) {
			setViewField = false;
			CmdSetViewField (toggleViewField, turretID, gameObjectName);
		}

		if (newTurretFollow && numberInQueue == 2) {
			CmdSendTurretCoordinates (xPercent, yPercent);
		}

		if (pickedUp && numberInQueue == 2) {
			pickedUp = false;
			CmdSetFollowBool ();
		}

		if (spawnCircle && numberInQueue == 2) {
			spawnCircle = false;
			CmdSpawnCircle (xPercent, yPercent);
		}

		if (winGame && numberInQueue == 1) {
			winGame = false;
			CmdWinScene ();
		}

		if (updateHealth && numberInQueue == 1) {
			updateHealth = false;
			CmdHealthChangeThing (healthChange);

		}

		if (gameOver && numberInQueue == 2) {
			gameOver = false;
			CmdSceneChange ();
		}

		if (updateResources && numberInQueue == 1) {
			updateResources = false;
			CmdAddResources (resourcesAdd);
		}

		//2 is the shop 1 is the gamescene

		if (shouldSpawnTurret && numberInQueue == 2) {
			shouldSpawnTurret = false;
			CmdSpawnTurret (nameOfTurretToSpawn);
		}

		if (shouldSetFollowing && numberInQueue == 1) {
			shouldSetFollowing = false;
			CmdSetFollow (toSetFollowing);
		}

		if (updateSellPrice && numberInQueue == 2) {
			updateSellPrice = false;
			CmdSetSellPrice (sellPrice);
		}

		if (updateTargeting && numberInQueue == 2) {
			updateTargeting = false;
			CmdUpdateTargeting (currentTargeting);
		}

		if (isSold && numberInQueue == 2) {
			isSold = false;
			CmdSell (turretInstanceName);
		}

		if (upgrade1Bought && numberInQueue == 2) {
			upgrade1Bought = false;
			CmdUpdateUpgrade1 (upgrade1Level, upgrade1Price, currentDamage);
		}

		if (upgrade2Bought && numberInQueue == 2) {
			upgrade2Bought = false;
			CmdUpdateUpgrade2 (upgrade2Level, upgrade2Price, currentRadius);
		}

		if (toggleClicked && numberInQueue == 1) {
			toggleClicked = false;
			CmdToggleClicked (clickedTurret, turretResourceName, upgrade1Level, upgrade2Level, sellPrice, currentTargeting, totalSpent, turretKills);
		}
	}

	public GameObject playerPrefab;

	public int numberInQueue;
	public string name;

	[Command]
	void CmdUpdateDeath(bool dead)
	{
		RpcUpdateDeath (dead);
	}

	[ClientRpc]
	void RpcUpdateDeath(bool dead)
	{
		if (SceneManager.GetActiveScene ().name == "UIScene") {
			GameManager.gameManager.GetComponent<GameManager> ().isDead = dead;
		}
	}

	[Command]
	void CmdUpdateRes(bool isRessed)
	{
		RpcUpdateRes (isRessed);
	}

	[ClientRpc]
	void RpcUpdateRes(bool isRessed)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameObject player = GameObject.Find ("playerMove");
			player.GetComponent<MovementTest> ().enabled = true;
			player.GetComponent<MovementTest> ().health = GameManager.gameManager.GetComponent<GameManager> ().player2FullHealth;
			player.transform.Find ("playerSprite").GetComponent<RotTest> ().enabled = true;
			player.transform.Find ("playerSprite").GetComponent<SpriteRenderer> ().color = Color.white;
			GameManager.gameManager.GetComponent<GameManager> ().updatedHealth = false;
		}
	}

	[Command]
	void CmdPlaceBomb(float xPos, float yPos)
	{
		RpcPlaceBomb (xPos, yPos);
	}

	[ClientRpc]
	void RpcPlaceBomb (float xPos, float yPos)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameObject circleSpawn = GameObject.Find ("circleSpawner");
			GameObject localBomb = Resources.Load ("bomb") as GameObject;

			Instantiate(localBomb, new Vector3(
				(circleSpawn.GetComponent<circleSpawnScript> ().xRes * xPos), 
				(circleSpawn.GetComponent<circleSpawnScript> ().yRes * yPos),
				(localBomb.transform.position.z)), localBomb.transform.rotation);
		}
	}

	[Command]
	void CmdIncreasePlayer2Health(int amountToIncrease)
	{
		RpcIncreasePlayer2Health (amountToIncrease);
	}

	[ClientRpc]
	void RpcIncreasePlayer2Health(int amountToIncrease)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			Instantiate(Resources.Load("healingAnim"));
			GameObject player = GameObject.Find ("playerMove");
			if (GameManager.gameManager.GetComponent<GameManager> ().player2CurrentHealth + amountToIncrease >= GameManager.gameManager.GetComponent<GameManager> ().player2FullHealth) {
				player.GetComponent<MovementTest> ().health = GameManager.gameManager.GetComponent<GameManager> ().player2FullHealth;
			} else {
				player.GetComponent<MovementTest> ().health = GameManager.gameManager.GetComponent<GameManager> ().player2CurrentHealth + amountToIncrease;
			}
		}
	}

	[Command]
	void CmdUpdatePlayer2Health(bool updatePlayer2HealthBool)
	{
		RpcUpdatePlayer2Health (updatePlayer2HealthBool);
	}

	[ClientRpc]
	void RpcUpdatePlayer2Health(bool updatePlayer2HealthBool)
	{
		if (SceneManager.GetActiveScene ().name == "UIScene") {
			Debug.Log (updatePlayer2Health);
			GameManager.gameManager.GetComponent<GameManager> ().isFullHealth = updatePlayer2HealthBool;
		}
	}


	[Command]
	void CmdChangeTargetPos (float XPos, float YPos, int turretNum)
	{
		RpcChangeTargetPos (XPos, YPos, turretNum);
	}

	[ClientRpc]
	void RpcChangeTargetPos (float XPos, float YPos, int turretNum)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameObject[] activeTurrets;
			activeTurrets = GameObject.FindGameObjectsWithTag ("turret");
			foreach (GameObject theTurrets in activeTurrets) {
				if (theTurrets.GetComponent<ShootScript> ().turretID == turretNum) {
					GameObject circleSpawn = GameObject.Find ("circleSpawner");
					theTurrets.transform.Find("ViewField").transform.position = new Vector3(
						(circleSpawn.GetComponent<circleSpawnScript> ().xRes * XPos), 
						(circleSpawn.GetComponent<circleSpawnScript> ().yRes * YPos),
						(GameManager.gameManager.turret.transform.position.z));
				}
			}
		}
	}

	[Command]
	void CmdUpdateTurretDirection (int turretNum, float angle)
	{
		RpcUpdateTurretDirection (turretNum, angle);
	}

	[ClientRpc]
	void RpcUpdateTurretDirection(int turretNum, float angle)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameObject[] activeTurrets;
			activeTurrets = GameObject.FindGameObjectsWithTag ("turret");
			foreach (GameObject theTurrets in activeTurrets) {
				if (theTurrets.GetComponent<ShootScript> ().turretID == turretNum) {
					theTurrets.transform.Find("turretGun").transform.rotation = Quaternion.Euler (0.0f, 0.0f, angle - 90f);
				}
			}
		}
	}

	[Command]
	void CmdSendTurretCoordinates(float xLoc, float yLoc)
	{
		RpcSendTurretCoordinates (xLoc, yLoc);
	}

	[ClientRpc]
	void RpcSendTurretCoordinates(float xLoc, float yLoc)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameObject circleSpawn;
			circleSpawn = GameObject.Find ("circleSpawner");
			if (GameManager.gameManager.turret != null) {
				GameManager.gameManager.turret.transform.position = new Vector3 (
					(circleSpawn.GetComponent<circleSpawnScript> ().xRes * xLoc), 
					(circleSpawn.GetComponent<circleSpawnScript> ().yRes * yLoc),
					(GameManager.gameManager.turret.transform.position.z));
			}
		}
	}

	[Command]
	void CmdTryToStartRound()
	{
		RpcTryToStartRound ();
	}

	[ClientRpc]
	void RpcTryToStartRound()
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameObject.Find ("Canvas/PlayButton").GetComponent<startRound> ().startRoundButtonCommand ();
		}
	}

	[Command]
	void CmdUpdateKills()
	{
		RpcUpdateKills ();
	}

	[ClientRpc]
	void RpcUpdateKills()
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			if (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject != null) {
				turretKills = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().kills;
				CmdUpdateClientKills (turretKills);
			}
		}
	}

	[Command]
	void CmdUpdateWave(bool isWave, int currentWave)
	{
		RpcUpdateWave (isWave, currentWave);
	}

	[ClientRpc]
	void RpcUpdateWave(bool isWave, int currentWave)
	{
		if (SceneManager.GetActiveScene ().name == "UIScene") {
			GameManager.gameManager.GetComponent<GameManager> ().waveOngoing = isWave;
			GameManager.gameManager.GetComponent<GameManager> ().Wave = currentWave;
		}
	}

	[Command]
	void CmdUpdateTiles(string theTile)
	{
		RpcUpdateTiles (theTile);
	}

	[ClientRpc] 
	void RpcUpdateTiles(string theTile)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameObject tile = GameObject.Find ("Squares/" + theTile);

			if (tile != null) {
				tile.gameObject.GetComponent<SpriteRenderer> ().color = new Color (tile.gameObject.GetComponent<SpriteRenderer> ().color.r, tile.gameObject.GetComponent<SpriteRenderer> ().color.g, tile.gameObject.GetComponent<SpriteRenderer> ().color.b, 0f);
				tile.gameObject.GetComponent<Collider2D> ().enabled = false;
			}
		}
	}

	[Command]
	void CmdUpdateClientKills(int kills)
	{
		RpcUpdateClientKills (kills);
	}

	[ClientRpc]
	void RpcUpdateClientKills(int kills)
	{
		turretKills = kills;
	}

	[Command]
	void CmdSetViewField (bool viewBool, int turretNum,  string objName)
	{
		RpcSetViewField (viewBool, turretNum, objName); 
	}

	[ClientRpc]
	void RpcSetViewField(bool viewBool, int turretNum, string objName)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameObject[] activeTurrets;
			activeTurrets = GameObject.FindGameObjectsWithTag ("turret");
			foreach (GameObject theTurrets in activeTurrets) {
				if (theTurrets.name == objName) {
					if (theTurrets.GetComponent<ShootScript> ().turretID == turretNum) {
						if (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject != null) {
							GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("ViewField").gameObject.SetActive (false);
						}

						theTurrets.transform.Find ("ViewField").gameObject.SetActive (viewBool);

						if (viewBool) {
							GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject = theTurrets.gameObject;
							GameManager.gameManager.GetComponent<GameManager> ().turret = theTurrets.gameObject;
						} else {
							//GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject = null;
							//GameManager.gameManager.GetComponent<GameManager> ().turret = null;
						}


						/*if (viewBool == false) {
							GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject = null;
						}*/
						break;
					}
				}
			}
		} else if (SceneManager.GetActiveScene ().name == "UIScene") {
			GameObject[] activeTurrets;
			activeTurrets = GameObject.FindGameObjectsWithTag ("turret");
			foreach (GameObject theTurrets in activeTurrets) {
				if (theTurrets.name == objName) {
					if (theTurrets.GetComponent<ShootScript> ().turretID == turretNum) {
						if (viewBool) {
							upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret = theTurrets.gameObject;
							upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = true;
						} else {
							upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret = null;
							upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = false;
						}
						break;
					}
				}
			}
		}
	}


	[Command]
	void CmdSetFollowBool()
	{
		RpcSetFollowBool ();
	}

	[ClientRpc]
	void RpcSetFollowBool()
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			if (GameManager.gameManager.GetComponent<GameManager> ().turret != null) {
				if (!newTurretFollow) {
					//GameManager.gameManager.GetComponent<GameManager> ().turret = transform.parent.gameObject;
					GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretShopSpawn> ().isClicked = true;
					GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretShopSpawn> ().followMouse = true;
					GameManager.gameManager.GetComponent<GameManager> ().following = true;
					// is this needed?
					//GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("ViewField").gameObject.SetActive (true);
				} else {
					//GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("ViewField").gameObject.SetActive (false);
					GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().enabled = true;
				}
				newTurretFollow = !newTurretFollow;
			}
		} 
		else if (SceneManager.GetActiveScene ().name == "UIScene") {
			newTurretFollow = !newTurretFollow;
		}
	}

	[Command]
	void CmdSpawnCircle(float xLocPercent, float yLocPercent)
	{
		RpcSpawnCircle (xLocPercent, yLocPercent);
	}

	[ClientRpc]
	void RpcSpawnCircle(float xLocPercent, float yLocPercent)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameObject circleSpawn = GameObject.Find ("circleSpawner");
			circleSpawn.GetComponent<circleSpawnScript> ().xPercent = xLocPercent;
			circleSpawn.GetComponent<circleSpawnScript> ().yPercent = yLocPercent;
			circleSpawn.GetComponent<circleSpawnScript> ().shouldSpawn = true;
		} 
	}

	[Command]
	void CmdWinScene ()
	{
		SceneManager.LoadScene (3);
		RpcWinScene ();
	}

	[ClientRpc]
	void RpcWinScene()
	{
		SceneManager.LoadScene (3);
	}

	[Command]
	void CmdSetName(int number)
	{
		this.gameObject.name = "localPlayer" + number.ToString ();
		RpcSetName (numberInQueue);
	}

	//[ClientRpc]
	void RpcSetName (int number)
	{
		this.gameObject.name = "localPlayer" + number.ToString ();
	}

	[Command]
	void CmdAddResources (int toAdd)
	{
		RpcAddResources (toAdd);
	}

	[ClientRpc]
	void RpcAddResources(int toAdd)
	{
		if (SceneManager.GetActiveScene ().name == "UIScene") {
			GameManager.gameManager.GetComponent<GameManager> ().Resource += toAdd;
		}
	}

	[Command]
	void CmdHealthChangeThing(int healthToChange)
	{
		RpcHealthChangeThing (healthToChange);
	}

	[ClientRpc]
	void RpcHealthChangeThing(int healthToChange)
	{
		if (SceneManager.GetActiveScene ().name == "UIScene") {
			GameManager.gameManager.GetComponent<GameManager> ().Health -= healthToChange;

			if (GameManager.gameManager.GetComponent<GameManager> ().Health <= 0) {
				SceneManager.LoadSceneAsync (4);
			}
		}
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameManager.gameManager.GetComponent<GameManager> ().Health -= healthToChange;

			if (GameManager.gameManager.GetComponent<GameManager> ().Health <= 0) {
				SceneManager.LoadSceneAsync(4);
			}
		}
	}

	[Command]
	void CmdSceneChange()
	{
		SceneManager.LoadScene (4);
		RpcSceneChange ();
	}

	[ClientRpc]
	void RpcSceneChange()
	{
		SceneManager.LoadScene (4);
	}

	[Command]
	void CmdSpawnTurret(string turretToSpawn)
	{
		//gameManager.GetComponent<gameManagerScript> ().setStaticInstance (toSwitch, true, color);
		RpcSpawnTurret (turretToSpawn);
	}

	[ClientRpc]
	void RpcSpawnTurret(string turretToSpawn)
	{
		// if the active scene is the game scene, spawn a turret
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameObject tempObj;
			GameObject toLoad = Resources.Load (turretToSpawn) as GameObject;
			// set its name to turret + number.ToString(), then update number
			// pass this value to here to make sure it knows what turret is being
			// deleted or when something is upgraded
			tempObj = Instantiate (toLoad, new Vector3 (4.58f, -5.3f, 1f), transform.rotation);
			tempObj.GetComponent<ShootScript> ().turretID = GameManager.gameManager.GetComponent<GameManager> ().turretCount;
			GameManager.gameManager.GetComponent<GameManager> ().turretCount++;
			if (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject != null) {
				GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("ViewField").gameObject.SetActive (false);
				GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject = null;
			}
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject = tempObj;
		}
		if (SceneManager.GetActiveScene ().name == "UIScene")
		{
			GameObject background = GameObject.Find ("Canvas/UIHolder/shopBackground");
			GameObject shopUI = GameObject.Find ("Canvas/UIHolder/shopPanel");
			GameObject gameCanvas = GameObject.Find ("Canvas");

			GameManager.gameManager.GetComponent<GameManager> ().shopOpen = false;

			GameObject map = GameObject.Find ("activeEnemy");

			var turrets = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.tag == "turret");

			foreach(GameObject theTurrets in turrets)
			{
				theTurrets.SetActive (true);
			}

			background.SetActive (false);
			shopUI.SetActive (false);
			gameCanvas.transform.Find ("PlayButton").gameObject.SetActive (true);
			gameCanvas.transform.Find ("shopButton").gameObject.SetActive (true);
			map.transform.Find("enemyPath").gameObject.SetActive (true);

			GameObject toSpawn;
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = false;
			GameObject toLoad = Resources.Load (turretToSpawn) as GameObject;
			// set its name to turret + number.ToString(), then update number
			// pass this value to here to make sure it knows what turret is being
			// deleted or when something is upgraded
			toSpawn = Instantiate (toLoad, new Vector3 (3.264152f, -1.863f, 1f), transform.rotation);
			toSpawn.transform.localScale = new Vector3(0.7f, 0.7f, toLoad.transform.localScale.z);
			toSpawn.GetComponent<ShootScript> ().turretID = GameManager.gameManager.GetComponent<GameManager> ().turretCount;
			GameManager.gameManager.GetComponent<GameManager> ().turretCount++;
			if (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject != null) {
				GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("ViewField").gameObject.SetActive (false);
			}
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject = toLoad;
		}
	}

	[Command]
	void CmdSetFollow(bool follow)
	{
		RpcSetFollow (follow);
	}

	[ClientRpc]
	void RpcSetFollow(bool follow)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameManager.gameManager.GetComponent<GameManager> ().following = follow;
		} else if (SceneManager.GetActiveScene ().name == "UIScene") {
			GameManager.gameManager.GetComponent<GameManager> ().following = follow;
		}
	}

	[Command]
	void CmdSetSellPrice(int toSell)
	{
		RpcSellPrice (toSell);
	}

	[ClientRpc]
	void RpcSellPrice(int toSell)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {

		} else if (SceneManager.GetActiveScene ().name == "UIScene") {

		}
	}

	[Command]
	void CmdUpdateTargeting(string toTarget)
	{
		RpcUpdateTargeting (toTarget);
	}

	[ClientRpc]
	void RpcUpdateTargeting(string toTarget)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().currentTargeting = toTarget;
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().target = toTarget;
		} else if (SceneManager.GetActiveScene ().name == "UIScene") {
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().currentTargeting = toTarget;
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().target = toTarget;
		}
	}

	[Command]
	void CmdSell(string random)
	{
		RpcSell (random);
	}

	[ClientRpc]
	void RpcSell(string random)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("ViewField").GetComponent<setSize>().deleteAll ();
			Destroy (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject);
			GameManager.gameManager.GetComponent<GameManager> ().following = false;
		} else if (SceneManager.GetActiveScene ().name == "UIScene") {
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("ViewField").GetComponent<setSize>().deleteAll ();
			Destroy (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject);
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = false;
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().sellButton.GetComponent<Outline> ().enabled = false;
			GameManager.gameManager.GetComponent<GameManager> ().following = false;
		}
	}

	[Command]
	void CmdUpdateUpgrade1(int upgradeLevel, int upgradePrice, int newDamage)
	{
		RpcUpdateUpgrade1 (upgradeLevel, upgradePrice, newDamage);
	}

	[ClientRpc]
	void RpcUpdateUpgrade1(int upgradeLevel, int upgradePrice, int newDamage)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
				GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 = upgradeLevel;
				GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().totalTurretCost += upgradePrice;
				GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<ShootScript> ().damage = newDamage;
		} else if (SceneManager.GetActiveScene ().name == "UIScene") {
			
		}
	}

	[Command]
	void CmdUpdateUpgrade2(int upgradeLevel, int upgradePrice, float newRadius)
	{
		RpcUpdateUpgrade2 (upgradeLevel, upgradePrice, newRadius);
	}

	[ClientRpc]
	void RpcUpdateUpgrade2(int upgradeLevel, int upgradePrice, float newRadius)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 = upgradeLevel;
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretUpgradeScript> ().totalTurretCost += upgradePrice;
			//DestroyImmediate (GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("visionCollider").GetComponent<CircleCollider2D> ());
			//CircleCollider2D newCollider = GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("visionCollider").gameObject.AddComponent <CircleCollider2D>();
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("visionCollider").GetComponent <CircleCollider2D>().radius = newRadius;
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("visionCollider").GetComponent <CircleCollider2D>().isTrigger = true;
			//newCollider.radius = newRadius;
			//newCollider.isTrigger = true;
		} else if (SceneManager.GetActiveScene ().name == "UIScene") {
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("visionCollider").GetComponent <CircleCollider2D>().radius = newRadius;
			GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.transform.Find ("visionCollider").GetComponent <CircleCollider2D>().isTrigger = true;
		}
	}

	[Command]
	void CmdKillCount(int kills)
	{
		RpcKillCount(kills);
	}

	[ClientRpc]
	void RpcKillCount(int kills)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {

		} else if (SceneManager.GetActiveScene ().name == "UIScene") {

		}
	}

	[Command]
	void CmdTurretImage(string image)
	{
		RpcTurretImage(image);
	}

	[ClientRpc]
	void RpcTurretImage(string image)
	{
		if (SceneManager.GetActiveScene ().name == "UIScene") {

		}
	}

	[Command]
	void CmdTurretName(string name)
	{
		RpcTurretName(name);
	}

	[ClientRpc]
	void RpcTurretName(string name)
	{
		if (SceneManager.GetActiveScene ().name == "UIScene") {

		}
	}

	[Command]
	void CmdTurretPrice(int price)
	{
		RpcTurretPrice(price);
	}

	[ClientRpc]
	void RpcTurretPrice(int price)
	{
		if (SceneManager.GetActiveScene ().name == "UIScene") {
			
		}
	}

	[Command]
	void CmdTurretInstanceName(string instanceName)
	{
		RpcTurretInstanceName(instanceName);
	}

	[ClientRpc]
	void RpcTurretInstanceName(string instanceName)
	{
		if (SceneManager.GetActiveScene ().name == "GameScene") {

		} else if (SceneManager.GetActiveScene ().name == "UIScene") {

		}
	}
		
	[Command]
	void CmdToggleClicked(bool hit, string turretName, int upgrade1, int upgrade2, int price, string target, int totalCost, int enemiesKilled)
	{
		RpcToggleClicked(hit, turretName, upgrade1, upgrade2, price, target, totalCost, enemiesKilled);
	}

	[ClientRpc]
	void RpcToggleClicked(bool hit, string turretName, int upgrade1, int upgrade2, int price, string target, int totalCost, int enemiesKilled)
	{
		if (SceneManager.GetActiveScene ().name == "UIScene") {
			if (hit) {
				GameObject tempTurret = Resources.Load (turretName) as GameObject;
				if (tempTurret != null) {
					/*tempTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 = upgrade1;
					tempTurret.GetComponent<turretUpgradeScript> ().currentUpgradeLevel2 = upgrade2;
					tempTurret.GetComponent<turretUpgradeScript> ().totalTurretCost = totalCost;
					tempTurret.GetComponent<turretUpgradeScript> ().currentTargeting = target;
					tempTurret.GetComponent<turretUpgradeScript>().killAmount = enemiesKilled;*/

					upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret = tempTurret;
					upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = true;
				}
			} else {
				upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret = null;
				upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = false;
			}
		}
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			/*if (hit) {
				GameManager.gameManager.GetComponent<GameManager>().activelyPressedObject.transform.Find("ViewField").gameObject.SetActive(true);
			} else {
				GameManager.gameManager.GetComponent<GameManager>().activelyPressedObject.transform.Find("ViewField").gameObject.SetActive(false);
			}*/
		}
	}

	[Command]
	void CmdSpawnMyUnit()
	{
		// unit exists in server now
		Debug.Log ("Start spawning object for player : " + NetworkServer.connections.Count.ToString());
		GameObject temp = Instantiate (playerPrefab);

		CmdSetPlayerNumber (NetworkServer.connections.Count, "localPlayer" + NetworkServer.connections.Count.ToString());
		if (NetworkServer.connections.Count == 1) {
			Debug.Log ("Loaded Scene 1");
			NetworkManager.networkSceneName = "UIScene";
		} 
		//temp.name = "localPlayer" + NetworkServer.connections.Count.ToString ();
		NetworkServer.SpawnWithClientAuthority (temp, connectionToClient);
	}

	[Command]
	void CmdSetPlayerNumber(int number, string mName)
	{
		numberInQueue = number;
		name = mName;
		gameObject.name = name;
		RpcSetPlayerNumber (number, mName);
	}

	[ClientRpc]
	void RpcSetPlayerNumber (int number, string mName)
	{
		name = mName;
		gameObject.name = name;
		numberInQueue = number;
	}
}