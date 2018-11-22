using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager gameManager;

	public List<GameObject> theTiles = new List<GameObject>();
	public List<GameObject> tileButtons = new List<GameObject>();
	public bool waveOngoing = false;
	public bool shopOpen = false;
	public int currentPrice = 200; 

	public bool isDead = false;

	public bool holdingBomb = false;

	public AudioClip secondMusic;

	GameObject bombText;

	public int player2FullHealth;
	public int player2CurrentHealth;
	bool updatedFullHealth = false;
	public bool updatedHealth = false;
	public bool isFullHealth = true;

	public GameObject flowerResource;

	// spawning for resource flowers
	public int maxResourceFlowers = 3;
	public int currentResourceFlowers = 0;
	public bool hasSpawned = false;

    public Text waveText;
    public Text resourceText;
    public bool gameOver = false;
    private int wave;

	public int turretCount = 0;

	public bool overShop = false;

	[SerializeField]
    private float resources;


	int blocksSpawned = 0;

	public int health;

	// variables to spawn map
	GameObject mainCamera;
	//GameObject movementNodes;
	GameObject pathingFloor;
	//GameObject otherFloor;
	GameObject pathObject;
	GameObject wayPointObj;
	public GameObject turret;
	public bool following;
	GameObject waveOngGoingText;

	GameObject healthText;

	public List<GameObject> enemyFloorSpawns = new List<GameObject>();

	public GameObject activelyPressedObject = null;

	//public GameObject pathingFloorPrefab;

	float xMin;
	float xMax;
	float yMin;
	float yMax;


	public int Health
	{
		get {
			return health;
		}
		set {
			
			health = value;
			Debug.Log (health);
			if (SceneManager.GetActiveScene ().name == "UIScene") {
				healthText = GameObject.Find ("Canvas/UIHolder/shopPanel/topUIHolder/currentHealth");
				healthText.GetComponent<Text> ().text = health.ToString ();
			}
		}
	}

    public int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            wave = value;
            waveText.text = "Wave: " + (wave + 1);
        }
    }

    public float Resource
    {
        get
        {
            return resources;
        }
        set
        {
            resources = value;
            resourceText.text = resources.ToString("F0");
        }
    }

	public bool getMoney(float amount)
	{
		if (amount <= resources) {
			return true;
		}
		return false;
	}

	void Start()
	{
		gameManager = this;
		//DontDestroyOnLoad (this);

		// initial resources set here
		Resource += 1000;

        //Time.timeScale = 100.0f;
	}
		
    // Use this for initialization
    void OnEnable () {
		SoundManager.soundManager.GetComponent<SoundManager> ().changeMusic (secondMusic);
		health = 100;
		following = false;
		// once they click on something in the shop disable the option to reset 
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			player2FullHealth = GameObject.Find ("playerMove").GetComponent<MovementTest> ().health;
			player2CurrentHealth = player2FullHealth;
			following = false;

			// sets speed of game
			//Time.timeScale = 5.0f;
			Wave = -1;
			Resource = 0;
			turret = Resources.Load ("turret") as GameObject;
			pathObject = GameObject.Find ("Path");
			mainCamera = GameObject.Find ("Main Camera");
			//movementNodes =  Resources.Load("WayPoint") as GameObject;
			pathingFloor = Resources.Load ("EnemyArea") as GameObject;
			//otherFloor = Resources.Load ("WalkingArea") as GameObject;
			wayPointObj = Resources.Load ("WayPoint") as GameObject;

			xMin = mainCamera.transform.position.x - 1.99f * mainCamera.GetComponent<Camera> ().orthographicSize; 

			// -3 works for the size of shop panel
			xMax = mainCamera.transform.position.x + 1.99f * mainCamera.GetComponent<Camera> ().orthographicSize + 1f;

			// +2 works for the size of the upgrade panel
			yMin = mainCamera.transform.position.y - mainCamera.GetComponent<Camera> ().orthographicSize + pathingFloor.GetComponent<SpriteRenderer> ().bounds.extents.y;
			yMax = mainCamera.transform.position.y + mainCamera.GetComponent<Camera> ().orthographicSize - pathingFloor.GetComponent<SpriteRenderer> ().bounds.extents.y;

			//spawnMap ();
		}

		if (SceneManager.GetActiveScene ().name == "UIScene") {
			waveOngGoingText = GameObject.Find ("Canvas/UIHolder/waveOngoingText");
			bombText = GameObject.Find ("bombText");
			mainCamera = GameObject.Find ("Main Camera");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// if over a UI element, do not check mouse collision
		/*if (EventSystem.current.IsPointerOverGameObject() ){
			return;
		}*/
		/*if (SceneManager.GetActiveScene ().name == "UIScene") {
			if (!GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretShopSpawn> ().isClicked && GameManager.gameManager.GetComponent<GameManager> ().activelyPressedObject.GetComponent<turretShopSpawn> ().followMouse) {
				//upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = false;
			}
		}*/
	if (SceneManager.GetActiveScene ().name == "GameScene") {
		player2CurrentHealth = GameObject.Find ("playerMove").GetComponent<MovementTest> ().health;

		if (player2CurrentHealth < player2FullHealth && !updatedFullHealth && player2CurrentHealth > 0) {
			GameObject localPlayer1 = GameObject.Find ("localPlayer1");
			if (localPlayer1 != null) {
				updatedFullHealth = true;
				updatedHealth = false;
				localPlayer1.GetComponent<networkPlayerScript> ().fullHealth = false;
				localPlayer1.GetComponent<networkPlayerScript> ().updatePlayer2Health = true;
				isDead = false;
			}
			// send to network that player's health is notfull (Set it to false)
			// then set the update bool
		} else if (player2CurrentHealth == player2FullHealth && !updatedHealth) {
			GameObject localPlayer1 = GameObject.Find ("localPlayer1");
			if (localPlayer1 != null) {
				updatedFullHealth = false;
				updatedHealth = true;
				localPlayer1.GetComponent<networkPlayerScript> ().fullHealth = true;
				localPlayer1.GetComponent<networkPlayerScript> ().updatePlayer2Health = true;
				isDead = false;
			}
			// send to network that layer's health is full (Set it to true)
			// then set the update bool
		} else if(player2CurrentHealth <= 0 && !isDead) {
			GameObject player = GameObject.Find ("playerMove");
			GameObject localPlayer1 = GameObject.Find ("localPlayer1");
			isDead = true;
			localPlayer1.GetComponent<networkPlayerScript> ().isDead = true;
			localPlayer1.GetComponent<networkPlayerScript> ().updateDeath = true;
			player.GetComponent<MovementTest> ().enabled = false;
			//Debug.Log ("DEADD");
			player.transform.Find ("playerSprite").GetComponent<RotTest> ().enabled = false;
			player.transform.Find ("playerSprite").GetComponent<SpriteRenderer> ().color = new Color (Color.gray.r, Color.gray.g, Color.gray.b, 167f / 255f);
		}
	}

	if (SceneManager.GetActiveScene ().name == "UIScene") {
		if (holdingBomb) {
				if (!IsPointerOverUIObject ()) {
					if (Input.GetMouseButton (0)) {
						Vector3 mousePosition = Input.mousePosition;
						mousePosition.z = 5f;

						Vector2 v = Camera.main.ScreenToWorldPoint (mousePosition);

						List<Collider2D> colList = new List<Collider2D> (); 

						//Collider2D[] col = Physics2D.OverlapPointAll(v);
						Collider2D[] col = Physics2D.OverlapCircleAll (v, 0.5f);

						for (int x = 0; x < col.Length; x++) {
							if (col [x].isTrigger || (col [x].name.CompareTo ("visionCollider") == 0) || ((col [x].name.CompareTo ("TurretBodyCollider") == 0)) || (col [x].tag.CompareTo ("enemyArea") == 0)) {
							} else {
								colList.Add (col [x]);
								//Debug.Log (col [x].gameObject.name);
							}
						}

						if (colList.Count > 0) {
							//activeTurret.transform.Find ("ViewField").GetComponent<SpriteRenderer> ().color = new Color (1f, 0f, 0f, 101f / 255f);
							//Debug.Log ("CANT PLACE");
							bombText.GetComponent<Text> ().text = "Bomb can't be placed there!";
							// change place text here to can't place there! Try again...
						} else {

							//Instantiate (turret, new Vector3 (v.x, v.y, 0f), transform.rotation);
							//turret.GetComponent<turretShopSpawn> ().followMouse = false;
							holdingBomb = false;
							//activeTurret.transform.Find ("ViewField").GetComponent<SpriteRenderer> ().color = new Color (161f / 255f, 160f / 255f, 160f / 255f, 101f / 255f);
							//Debug.Log ("CAN PLACE");
							bombText.GetComponent<Text> ().text = "";
							GameObject localPlayer2 = GameObject.Find ("localPlayer2");
							GameObject mapObj = GameObject.Find ("mapBounds");

							Vector2 mousePos = mainCamera.GetComponent<Camera> ().ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
							localPlayer2.GetComponent<networkPlayerScript>().xCoord = (mousePos.x)/mapObj.GetComponent<convertMouseClick>().xRes;
							localPlayer2.GetComponent<networkPlayerScript> ().yCoord = (mousePos.y + mapObj.GetComponent<convertMouseClick> ().yMin) / mapObj.GetComponent<convertMouseClick> ().yRes;
							localPlayer2.GetComponent<networkPlayerScript> ().placeBomb = true;
							
							// make place text dissapear and set bomb bool to false
							// also send coordinates to be converted to place a bomb gameobject here in the actual game
						}
					}
				} else {
					//Debug.Log ("CANT PLACE");
					bombText.GetComponent<Text> ().text = "Bomb can't be placed there!";
				}
		}
	}

	if (waveOngoing && !hasSpawned && SceneManager.GetActiveScene().name == "GameScene") {
			hasSpawned = true;
			// only try to spawn if the current resource flower is less than max amount
			if (!(currentResourceFlowers == maxResourceFlowers)) {
				// +1 as random.range for ints is [x,y)
				int flowersToSpawn = Random.Range (1, maxResourceFlowers - currentResourceFlowers + 1);
				int spawnedFlowers = 0;
				GameObject circleObj = GameObject.Find ("circleSpawner");
				float xSpawnMin = (-circleObj.GetComponent<circleSpawnScript> ().xRes / 2.0f) + (flowerResource.GetComponent<CircleCollider2D>().bounds.extents.x*2f);
				float xSpawnMax = (circleObj.GetComponent<circleSpawnScript> ().xRes / 2.0f) - (flowerResource.GetComponent<CircleCollider2D>().bounds.extents.x*2f);
				float ySpawnMin = (-circleObj.GetComponent<circleSpawnScript> ().yRes / 2.0f) + (flowerResource.GetComponent<CircleCollider2D>().bounds.extents.y*2f);
				float ySpawnMax = (circleObj.GetComponent<circleSpawnScript> ().yRes / 2.0f) - (flowerResource.GetComponent<CircleCollider2D>().bounds.extents.y*2f);
				
				while (spawnedFlowers < flowersToSpawn) {
					float xSpawnLocation = Random.Range (xSpawnMin, xSpawnMax);
					float ySpawnLocation = Random.Range (ySpawnMin, ySpawnMax);

					List<Collider2D> colList = new List<Collider2D> (); 

					Collider2D[] col = Physics2D.OverlapCircleAll (new Vector2(xSpawnLocation, ySpawnLocation), flowerResource.GetComponent<CircleCollider2D>().radius);
					
					for (int x = 0; x < col.Length; x++) {
						if (col [x].gameObject.tag == "square") {
							
						} else {
							colList.Add (col [x]);
						}
					}


					// if nothing is there then spawn a flower
					if (colList.Count == 0) {
						Instantiate (flowerResource, new Vector3 (xSpawnLocation, ySpawnLocation, flowerResource.transform.position.z), transform.rotation);
						spawnedFlowers++;
						currentResourceFlowers++;
					}
				}
			}
		} else if (!waveOngoing && hasSpawned && SceneManager.GetActiveScene().name == "GameScene") {
			// reset for each round
			hasSpawned = false;
		}
		
		
		if (SceneManager.GetActiveScene ().name == "UIScene") {
			if (waveOngoing) {
			waveOngGoingText.GetComponent<Text>().text = "Wave Ongoing";
			} else {
				waveOngGoingText.GetComponent<Text>().text = "No wave Ongoing";
			}
			if (Wave+2 == 3 && !waveOngoing) {
				if (TurretShop.turretShop.GetComponent<TurretShop> ().turretList [1].unlocked == false) {
					TurretShop.turretShop.GetComponent<TurretShop> ().turretList [1].unlocked = true;
					GameObject unlockTurret = GameObject.Find ("Canvas/unlockTurretReference/unlockTurret");
					unlockTurret.transform.Find ("turretImage").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("ShopSprites/" + GameObject.Find("Canvas/UIHolder/shopPanel").GetComponent<TurretShop>().turretList[1].unlockedSprite);
					unlockTurret.transform.Find ("turretName").GetComponent<Text> ().text = GameObject.Find("Canvas/UIHolder/shopPanel").GetComponent<TurretShop>().turretList[1].turretName;
					unlockTurret.gameObject.SetActive (true);
				}
			}
			if (Wave+2 == 5 && !waveOngoing) {
				if (TurretShop.turretShop.GetComponent<TurretShop> ().turretList [2].unlocked == false) {
					{
						TurretShop.turretShop.GetComponent<TurretShop> ().turretList [2].unlocked = true;
						GameObject unlockTurret = GameObject.Find ("Canvas/unlockTurretReference/unlockTurret");
						unlockTurret.transform.Find ("turretImage").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("ShopSprites/" + GameObject.Find("Canvas/UIHolder/shopPanel").GetComponent<TurretShop>().turretList[2].unlockedSprite);
						unlockTurret.transform.Find ("turretName").GetComponent<Text> ().text = GameObject.Find("Canvas/UIHolder/shopPanel").GetComponent<TurretShop>().turretList[2].turretName;
						unlockTurret.gameObject.SetActive (true);
					}
				}
			}

			if (Wave+2 == 7 && !waveOngoing) {
				if (TurretShop.turretShop.GetComponent<TurretShop> ().turretList [3].unlocked == false) {
					{
						TurretShop.turretShop.GetComponent<TurretShop> ().turretList [3].unlocked = true;
						GameObject unlockTurret = GameObject.Find ("Canvas/unlockTurretReference/unlockTurret");
						unlockTurret.transform.Find ("turretImage").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("ShopSprites/" + GameObject.Find("Canvas/UIHolder/shopPanel").GetComponent<TurretShop>().turretList[3].unlockedSprite);
						unlockTurret.transform.Find ("turretName").GetComponent<Text> ().text = GameObject.Find("Canvas/UIHolder/shopPanel").GetComponent<TurretShop>().turretList[3].turretName;
						  unlockTurret.gameObject.SetActive (true);
					}
				}
			}

			if (Wave+2 == 10 && !waveOngoing) {
				if (TurretShop.turretShop.GetComponent<TurretShop> ().turretList [4].unlocked == false) {
					{
						TurretShop.turretShop.GetComponent<TurretShop> ().turretList [4].unlocked = true;
						GameObject unlockTurret = GameObject.Find ("Canvas/unlockTurretReference/unlockTurret");
						unlockTurret.transform.Find ("turretImage").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("ShopSprites/" + GameObject.Find("Canvas/UIHolder/shopPanel").GetComponent<TurretShop>().turretList[4].unlockedSprite);
						unlockTurret.transform.Find ("turretName").GetComponent<Text> ().text = GameObject.Find("Canvas/UIHolder/shopPanel").GetComponent<TurretShop>().turretList[4].turretName;
						unlockTurret.gameObject.SetActive (true);
					}
				}
			}

			if (Wave+2 == 13 && !waveOngoing) {
				if (TurretShop.turretShop.GetComponent<TurretShop> ().turretList [5].unlocked == false) {
					{
						TurretShop.turretShop.GetComponent<TurretShop> ().turretList [5].unlocked = true;
						GameObject unlockTurret = GameObject.Find ("Canvas/unlockTurretReference/unlockTurret");
						unlockTurret.transform.Find ("turretImage").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("ShopSprites/" + GameObject.Find("Canvas/UIHolder/shopPanel").GetComponent<TurretShop>().turretList[5].unlockedSprite);
						unlockTurret.transform.Find ("turretName").GetComponent<Text> ().text = GameObject.Find("Canvas/UIHolder/shopPanel").GetComponent<TurretShop>().turretList[5].turretName;
						unlockTurret.gameObject.SetActive (true);
					}
				}
			}
			// set the other turret unlock levels here
		}

		if (!waveOngoing && !shopOpen && SceneManager.GetActiveScene().name == "UIScene") {
			for (int x = 0; x < theTiles.Count; x++) {
				if (theTiles [x].GetComponent<hexagonScript> ().isBought || !theTiles[x].GetComponent<hexagonScript>().canBeBought) {
					tileButtons [x].SetActive (false);
				} else if(theTiles[x].GetComponent<hexagonScript>().isBought && !theTiles[x].GetComponent<hexagonScript>().isUpdated)
				{
					GameObject localPlayer2 = GameObject.Find ("localPlayer2");
					theTiles [x].GetComponent<hexagonScript> ().isUpdated = true;
					localPlayer2.GetComponent<networkPlayerScript> ().tileBought = theTiles[x].name;
					localPlayer2.GetComponent<networkPlayerScript> ().boughtATile = true;
				}
				else{
					tileButtons [x].SetActive (true);
					tileButtons[x].transform.Find("price").GetComponent<Text>().text = "$ " + currentPrice.ToString();
				}
			}
		} else {
			for (int x = 0; x < theTiles.Count; x++) {
				tileButtons [x].SetActive (false);
			}
		}

		if (!IsPointerOverUIObject()/*!EventSystem.current.IsPointerOverGameObject ()*/) {
			//Debug.Log (EventSystem.current.currentSelectedGameObject.name);


			if (turret != null && Input.GetMouseButton (0) && SceneManager.GetActiveScene ().name == "UIScene" && !turret.GetComponent<turretShopSpawn> ().followMouse) {
				GameObject localPlayer2;
				localPlayer2 = GameObject.Find ("localPlayer2");
				Vector3 mousePosition = Input.mousePosition;
				Vector2 v = Camera.main.ScreenToWorldPoint (mousePosition);
				Collider2D[] col = Physics2D.OverlapPointAll (v);
				List<Collider2D> colList = new List<Collider2D> (); 

				if (col.Length == 0) {
					localPlayer2.GetComponent<networkPlayerScript> ().clickedTurret = false;
					localPlayer2.GetComponent<networkPlayerScript> ().toggleClicked = true;
					if (activelyPressedObject != null) {
						localPlayer2.GetComponent<networkPlayerScript> ().toggleViewField = false;
						localPlayer2.GetComponent<networkPlayerScript> ().turretID = activelyPressedObject.GetComponent<ShootScript> ().turretID;
						localPlayer2.GetComponent<networkPlayerScript> ().gameObjectName = activelyPressedObject.gameObject.name;
						localPlayer2.GetComponent<networkPlayerScript> ().setViewField = true;
						activelyPressedObject.transform.Find ("ViewField").gameObject.SetActive (false);
						activelyPressedObject = null;
					}
					//upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = false;
					/*if (upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret != null) {
					upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret.transform.Find ("ViewField").gameObject.SetActive (false);
					upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret = null;
				}*/
					return;
				}

				for (int x = 0; x < col.Length; x++) {
				// if it is clicking a turret do not hide the shop
				if (col [x].gameObject.layer == 12 || col[x].gameObject.tag == "turretPivots") {
						return;
					} else {
						colList.Add (col [x]);
					}
				}

				if (colList.Count > 0) {
					localPlayer2.GetComponent<networkPlayerScript> ().clickedTurret = false;
					localPlayer2.GetComponent<networkPlayerScript> ().toggleClicked = true;
					if (activelyPressedObject != null) {
						activelyPressedObject.transform.Find ("ViewField").gameObject.SetActive (false);
						localPlayer2.GetComponent<networkPlayerScript> ().toggleViewField = false;
						localPlayer2.GetComponent<networkPlayerScript> ().turretID = activelyPressedObject.GetComponent<ShootScript> ().turretID;
						localPlayer2.GetComponent<networkPlayerScript> ().gameObjectName = activelyPressedObject.gameObject.name;
						localPlayer2.GetComponent<networkPlayerScript> ().setViewField = true;
						activelyPressedObject = null;
					}
					//upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = false;
					/*if (upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret != null) {
					upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret.transform.Find ("ViewField").gameObject.SetActive (false);
					upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret = null;
				}*/
					return;
				}
			}
		}
	}

	public bool CheckLocation(GameObject activeTurret)
	{
		if (activeTurret == null || turret == null) {
			return false;
		}
		if (!overShop) {
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = 5f;

			Vector2 v = Camera.main.ScreenToWorldPoint (mousePosition);

			List<Collider2D> colList = new List<Collider2D> (); 

			//Collider2D[] col = Physics2D.OverlapPointAll(v);
			Collider2D[] col = Physics2D.OverlapCircleAll (v, turret.transform.Find ("TurretBodyCollider").GetComponent<CircleCollider2D> ().radius / 2.0f);

			for (int x = 0; x < col.Length; x++) {
				if (col [x].isTrigger ||(col[x].name.CompareTo("visionCollider") == 0)|| ((col[x].name.CompareTo("TurretBodyCollider") == 0) && (col [x].gameObject.transform.parent.gameObject == activeTurret))) {
				} else {
					colList.Add (col [x]);
					//Debug.Log (col [x].gameObject.name);
				}
			}

			if (colList.Count > 0) {
				//activeTurret.transform.Find ("ViewField").GetComponent<SpriteRenderer> ().color = new Color (1f, 0f, 0f, 101f / 255f);
				//Debug.Log ("CANT PLACE");
				return false;
			} else {

				//Instantiate (turret, new Vector3 (v.x, v.y, 0f), transform.rotation);
				//turret.GetComponent<turretShopSpawn> ().followMouse = false;
				following = false;
				//activeTurret.transform.Find ("ViewField").GetComponent<SpriteRenderer> ().color = new Color (161f / 255f, 160f / 255f, 160f / 255f, 101f / 255f);
				//Debug.Log ("CAN PLACE");
				return true;
			}
		}
		return false;
	}

	void spawnMap()
	{
		GameObject toAdd;
		GameObject wayPointToAdd;
		float currentX = xMin;
		float currentY = mainCamera.transform.position.y;
		float newX = 0f;
		float newY = 0f;
		// startPoint
		wayPointToAdd = Instantiate(wayPointObj,  new Vector3 (xMin, mainCamera.transform.position.y, 0f), transform.rotation);
		pathObject.GetComponent<SpawnEnemy> ().waypoints.Add (wayPointToAdd);
		toAdd = Instantiate (pathingFloor, new Vector3 (xMin, mainCamera.transform.position.y, 1f), transform.rotation);
		toAdd.name = "BLOCK" + blocksSpawned.ToString ();
		blocksSpawned++;
		enemyFloorSpawns.Add (toAdd);
		while (currentX <= xMax) {
			if (currentX + 1f < xMax) {
				newX = currentX + (int)Random.Range (2f, 3f) * 2f * pathingFloor.GetComponent<SpriteRenderer> ().bounds.extents.x;
				newY = (int)Random.Range (yMin, yMax);

				toAdd = Instantiate (pathingFloor, new Vector3 (newX, newY, 1f), transform.rotation);
				toAdd.name = "BLOCK" + blocksSpawned.ToString ();
				blocksSpawned++;
				enemyFloorSpawns.Add (toAdd);
				currentX = newX;
			}
		}
		currentX = xMin;
		currentY = mainCamera.transform.position.y;
		for(int x = 0; x < enemyFloorSpawns.Count; x++)
		{
			if (x == enemyFloorSpawns.Count - 1) {
				break;
			}

			wayPointToAdd = Instantiate(wayPointObj,  new Vector3 (currentX, currentY, 0f), transform.rotation);
			pathObject.GetComponent<SpawnEnemy> ().waypoints.Add (wayPointToAdd);
			while (currentX < enemyFloorSpawns [x + 1].transform.position.x) {
				newX = currentX + 2f * pathingFloor.GetComponent<SpriteRenderer> ().bounds.extents.x;
				toAdd = Instantiate (pathingFloor, new Vector3 (newX, currentY, 1f), transform.rotation);
				toAdd.name = "BLOCK" + blocksSpawned.ToString ();
				blocksSpawned++;
				//enemyFloorSpawns.Add (toAdd);
				currentX = newX;
			}


			wayPointToAdd = Instantiate(wayPointObj,  new Vector3 (currentX, currentY, 0f), transform.rotation);
			pathObject.GetComponent<SpawnEnemy> ().waypoints.Add (wayPointToAdd);


			if (currentY < enemyFloorSpawns [x + 1].transform.position.y) {
				while (currentY < enemyFloorSpawns [x + 1].transform.position.y) {
					if (currentX+3f >= xMax && x == enemyFloorSpawns.Count-2) {
						break;
					}
					newY = currentY + 2f * pathingFloor.GetComponent<SpriteRenderer> ().bounds.extents.y;
					toAdd = Instantiate (pathingFloor, new Vector3 (currentX, newY, 1f), transform.rotation);
					toAdd.name = "BLOCK" + blocksSpawned.ToString ();
					blocksSpawned++;
					//enemyFloorSpawns.Add (toAdd);
					currentY = newY;
				}
			} else if (currentY > enemyFloorSpawns [x + 1].transform.position.y) {
				while (currentY > enemyFloorSpawns [x + 1].transform.position.y) {
					if (currentX+3f >= xMax && x == enemyFloorSpawns.Count-2) {
						break;
					}
					newY = currentY - 2f * pathingFloor.GetComponent<SpriteRenderer> ().bounds.extents.y;
					toAdd = Instantiate (pathingFloor, new Vector3 (currentX, newY, 1f), transform.rotation);
					toAdd.name = "BLOCK" + blocksSpawned.ToString ();
					blocksSpawned++;
					//enemyFloorSpawns.Add (toAdd);
					currentY = newY;
				}
			}
		}
	}

	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
}