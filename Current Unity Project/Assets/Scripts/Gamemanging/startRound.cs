using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startRound : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {

	GameObject path;

	// Use this for initialization
	void Start () {
		path = GameObject.Find ("Path");
		if (SceneManager.GetActiveScene ().name == "UIScene") {
			gameObject.GetComponent<Outline> ().enabled = false;
		}
	}

	bool canStartNewRound = true;

	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			GameObject[] enemiesInGame;
			GameObject localPlayer1 = GameObject.Find ("localPlayer1");
			enemiesInGame = GameObject.FindGameObjectsWithTag ("Enemy");

			int enemiesToSpawn = path.GetComponent<SpawnEnemy> ().spiderEnemiesList.Count + path.GetComponent<SpawnEnemy> ().tankEnemiesList.Count + path.GetComponent<SpawnEnemy> ().droneEnemiesList.Count + path.GetComponent<SpawnEnemy>().outsideEnemiesList.Count;

			if (enemiesInGame.Length == 0 && enemiesToSpawn == 0 && path.GetComponent<SpawnEnemy> ().waveCount == GameManager.gameManager.GetComponent<GameManager> ().Wave) {
				canStartNewRound = true;
				GameManager.gameManager.GetComponent<GameManager> ().waveOngoing = false;
				localPlayer1.GetComponent<networkPlayerScript> ().waveOccuring = false;
				localPlayer1.GetComponent<networkPlayerScript> ().toggleWave = true;
				localPlayer1.GetComponent<networkPlayerScript> ().theWave = GameManager.gameManager.GetComponent<GameManager> ().Wave;
			} 
		}
	}
		
	public void tryToStartRound()
	{
		GameObject localPlayer2 = GameObject.Find ("localPlayer2");
		localPlayer2.GetComponent<networkPlayerScript> ().tryToStartRound = true;
	}

	public void startRoundButtonCommand()
	{
		if (canStartNewRound) {
			GameObject localPlayer1 = GameObject.Find ("localPlayer1");
			GameManager.gameManager.GetComponent<GameManager> ().waveOngoing = true;
			localPlayer1.GetComponent<networkPlayerScript> ().waveOccuring = true;
			localPlayer1.GetComponent<networkPlayerScript> ().toggleWave = true;
			localPlayer1.GetComponent<networkPlayerScript> ().theWave = GameManager.gameManager.GetComponent<GameManager> ().Wave;
			// make spawn time for rounds 0 seconds
			path.GetComponent<SpawnEnemy>().playPressed = true;
			canStartNewRound = false;
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