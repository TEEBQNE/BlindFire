using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tileButtonScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Outline> ().enabled = false;	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gameManager.GetComponent<GameManager> ().Resource < GameManager.gameManager.GetComponent<GameManager>().currentPrice) {
			gameObject.GetComponent<Image> ().color = new Color (155f/255f, 0f, 0f, gameObject.GetComponent<Image> ().color.a);
		} else {
			gameObject.GetComponent<Image> ().color = new Color (94f/255f, 41/255f, 117/255f, gameObject.GetComponent<Image> ().color.a);
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

	public void buyTile()
	{
		GameObject localPlayer2 = GameObject.Find ("localPlayer2");
		if (localPlayer2 != null && SceneManager.GetActiveScene ().name == "UIScene" && GameManager.gameManager.GetComponent<GameManager> ().Resource >= GameManager.gameManager.GetComponent<GameManager>().currentPrice) {
			GameObject tile = GameObject.Find ("activeEnemy/enemyPath/Squares/" + gameObject.name);
			GameManager.gameManager.GetComponent<GameManager> ().Resource -= GameManager.gameManager.GetComponent<GameManager> ().currentPrice;
			GameManager.gameManager.GetComponent<GameManager> ().currentPrice = (int)(GameManager.gameManager.GetComponent<GameManager> ().currentPrice *  1.2);
			tile.GetComponent<SpriteRenderer> ().color = new Color (tile.GetComponent<SpriteRenderer> ().color.r, tile.GetComponent<SpriteRenderer> ().color.g, tile.GetComponent<SpriteRenderer> ().color.b, 0f);
			tile.GetComponent<Collider2D> ().enabled = false;
			tile.GetComponent<hexagonScript> ().isBought = true;
			localPlayer2.GetComponent<networkPlayerScript> ().tileBought = gameObject.name;
			localPlayer2.GetComponent<networkPlayerScript> ().boughtATile = true;
		}
	}
}
