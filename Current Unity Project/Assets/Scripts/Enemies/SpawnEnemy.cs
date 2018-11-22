using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
	public GameObject droneEnemyPref;
	public GameObject tankEnemyPref;
	public GameObject spiderEnemyPref;
	public GameObject outsideEnemyPref;

	public int maxDroneEnemies = 15;
	public int maxTankEnemies = 15;
	public int maxSpiderEnemies = 15;
	public int maxOutsideEnemies = 15;

	public float droneSpawnInterval = 1;
	public float tankSpawnInterval = 1;
	public float spiderSpawnInterval = 1;
	public float outsideSpawnInterval = 1;
}

public class SpawnEnemy : MonoBehaviour {
	public static SpawnEnemy instance;
	public List<GameObject> waypoints;
	public List<GameObject> spawnPoints;
	public GameObject droneEnemy;
	public GameObject tankEnemy;
	public GameObject spiderEnemy;
	public GameObject outsideEnemy;

	public GameObject boss;

	public GameObject roundOngoingText;

	public List<GameObject> droneEnemiesList;
	public List<GameObject> tankEnemiesList;
	public List<GameObject> spiderEnemiesList;
	public List<GameObject> outsideEnemiesList;

	Vector3 spawnPoint;

	public Wave[] waves;

	public int waveInterval = 15;

	private GameManager gameManager;

	private float lastSpawnTime;
	private int droneEnemiesSpawned = 0;
	private int tankEnemiesSpawned = 0;
	private int spiderEnemiesSpawned = 0;
	private int outsideEnemiesSpawned = 0;

	public bool playPressed = false;

	public int waveCount = 0;

	bool hasSpawnedBoss = false;

	// Use this for initialization
	void Start () {
		roundOngoingText.GetComponent<Text> ().text = "No wave ongoing";
		instance = this;
		lastSpawnTime = Time.time;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		droneEnemiesList = new List<GameObject>();
		tankEnemiesList = new List<GameObject>();
		spiderEnemiesList = new List<GameObject>();
		outsideEnemiesList = new List<GameObject>();
	}

	// Update is called once per frame
	void Update () {

		if (playPressed) {
			if (gameManager.Wave < waves.Length - 1) {
				gameManager.Wave++;
				GameObject localPlayer1 = GameObject.Find ("localPlayer1");
				localPlayer1.GetComponent<networkPlayerScript> ().waveOccuring = true;
				localPlayer1.GetComponent<networkPlayerScript> ().toggleWave = true;
				localPlayer1.GetComponent<networkPlayerScript> ().theWave = GameManager.gameManager.GetComponent<GameManager> ().Wave;
				playPressed = false;
			} 
			/*else if((droneEnemiesSpawned == waves[currentWave].maxDroneEnemies && droneEnemiesList.Count == 0) && (tankEnemiesSpawned == waves[currentWave].maxTankEnemies && tankEnemiesList.Count == 0) && (spiderEnemiesSpawned == waves[currentWave].maxSpiderEnemies && spiderEnemiesList.Count == 0))
			{
				GameObject localPlayer1 = GameObject.Find ("localPlayer1");
				localPlayer1.GetComponent<networkPlayerScript> ().winGame = true;
			}
			*/
		}

		int currentWave = gameManager.Wave;

		if(!hasSpawnedBoss && gameManager.Wave == waves.Length-1 && (droneEnemiesSpawned == waves[currentWave].maxDroneEnemies && droneEnemiesList.Count == 0) && (tankEnemiesSpawned == waves[currentWave].maxTankEnemies && tankEnemiesList.Count == 0) && (spiderEnemiesSpawned == waves[currentWave].maxSpiderEnemies && spiderEnemiesList.Count == 0))
		{
			hasSpawnedBoss = true;
			Instantiate (boss, boss.transform.position, boss.transform.rotation);
			//GameObject localPlayer1 = GameObject.Find ("localPlayer1");
			//localPlayer1.GetComponent<networkPlayerScript> ().winGame = true;
		}


		// currently waves spawn after space is pressed. Set this to when a play button is pressed
		// also make the reset button turn off when the play button is first presse
		if(currentWave > -1 && currentWave < waves.Length && currentWave == waveCount)
		{
			roundOngoingText.GetComponent<Text> ().text = "Wave ongoing";
			float timeInterval = Time.time - lastSpawnTime;

			// Spawning of all drones in each wave is handled here
			float spawnInterval = waves[currentWave].droneSpawnInterval;
			if(((droneEnemiesSpawned == 0 && timeInterval > waveInterval) || (droneEnemiesSpawned > 0 && timeInterval > spawnInterval)) && droneEnemiesSpawned < waves[currentWave].maxDroneEnemies)
			{
				lastSpawnTime = Time.time;
				GameObject newDroneEnemy = Instantiate(waves[currentWave].droneEnemyPref);
				newDroneEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
				droneEnemiesList.Add(newDroneEnemy);
				droneEnemiesSpawned++;
			}         

			// Spawning of all tanks in each wave is handled here
			spawnInterval = waves[currentWave].tankSpawnInterval;
			if (((tankEnemiesSpawned == 0 && timeInterval > waveInterval) || (tankEnemiesSpawned > 0 && timeInterval > spawnInterval)) && tankEnemiesSpawned < waves[currentWave].maxTankEnemies)
			{
				lastSpawnTime = Time.time;
				GameObject newTankEnemy = Instantiate(waves[currentWave].tankEnemyPref);
				newTankEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
				tankEnemiesList.Add(newTankEnemy);
				tankEnemiesSpawned++;
			}

			// Spawning of all spiders in each wave is handled here
			spawnInterval = waves[currentWave].spiderSpawnInterval;
			if (((spiderEnemiesSpawned == 0 && timeInterval > waveInterval) || (spiderEnemiesSpawned > 0 && timeInterval > spawnInterval)) && spiderEnemiesSpawned < waves[currentWave].maxSpiderEnemies)
			{
				lastSpawnTime = Time.time;
				GameObject newSpiderEnemy = Instantiate(waves[currentWave].spiderEnemyPref);
				newSpiderEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
				spiderEnemiesList.Add(newSpiderEnemy);
				spiderEnemiesSpawned++;
			}

			// Spawning of all outside enemies in each wave is handled here
			if (((outsideEnemiesSpawned == 0 && timeInterval > waveInterval) || (outsideEnemiesSpawned > 0 && timeInterval > spawnInterval)) && outsideEnemiesSpawned < waves[currentWave].maxOutsideEnemies)
			{
				lastSpawnTime = Time.time;
				int randNum = (int)Random.Range(0, 3);
				int randEnemy = (int)Random.Range(0, 3);

				if (randNum == 0)
				{
					spawnPoint = new Vector3(-9.51f, 8.17f, 0);

				}
				else if (randNum == 1)
				{
					spawnPoint = new Vector3(6.98f, 7.47f, 0);
				}
				else if (randNum == 2)
				{
					spawnPoint = new Vector3(-11.94f, -3.7f, 0);
				}

				if (randEnemy == 0)
				{
					waves[currentWave].outsideEnemyPref = Resources.Load("WingEnemyMove") as GameObject;
				}
				else if (randEnemy == 1)
				{
					waves[currentWave].outsideEnemyPref = Resources.Load("RayEnemyMove") as GameObject;
				}
				else if (randEnemy == 2)
				{
					waves[currentWave].outsideEnemyPref = Resources.Load("FlyEnemyMove") as GameObject;
				}

				GameObject newOutsideEnemy = Instantiate(waves[currentWave].outsideEnemyPref, spawnPoint, Quaternion.identity);
				newOutsideEnemy.GetComponent<MoveOutsideEnemy>();
				outsideEnemiesList.Add(newOutsideEnemy);
				outsideEnemiesSpawned++;
			}

			if ((droneEnemiesSpawned == waves[currentWave].maxDroneEnemies && droneEnemiesList.Count == 0) && (tankEnemiesSpawned == waves[currentWave].maxTankEnemies && tankEnemiesList.Count == 0) && (spiderEnemiesSpawned == waves[currentWave].maxSpiderEnemies && spiderEnemiesList.Count == 0) && (outsideEnemiesSpawned == waves[currentWave].maxOutsideEnemies && outsideEnemiesList.Count == 0))
			{
				roundOngoingText.GetComponent<Text> ().text = "No wave ongoing";
				waveCount++;
				playPressed = false;
				droneEnemiesSpawned = 0;
				tankEnemiesSpawned = 0;
				spiderEnemiesSpawned = 0;
				outsideEnemiesSpawned = 0;
				lastSpawnTime = Time.time;
			}
		}
		else
		{
			gameManager.gameOver = true;
		}
	}
}