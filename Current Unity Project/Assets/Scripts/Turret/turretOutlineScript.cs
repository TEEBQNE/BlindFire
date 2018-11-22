using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class turretOutlineScript : MonoBehaviour {

	/*public Material basic;
	public Material outline;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnMouseOver()
	{
		if (transform.parent.GetComponent<turretShopSpawn>().followMouse == false) {
			//gameObject.transform.parent.GetComponent<SpriteRenderer> ().material = outline;
			gameObject.GetComponent<SpriteRenderer> ().material = outline;
		}

	}

	void OnMouseExit()
	{
		if (transform.parent.GetComponent<turretShopSpawn>().followMouse == false) {
			gameObject.GetComponent<SpriteRenderer> ().material = basic;
		}
	}

	void OnMouseDown()
	{
		if (!(EventSystem.current.IsPointerOverGameObject() )){

			if (GameManager.gameManager.GetComponent<GameManager> ().CheckLocation (transform.parent.gameObject)) {
				transform.parent.GetComponent<turretShopSpawn>().followMouse = false;
				transform.parent.GetComponent<ShootScript> ().enabled = true;
			}
		}
	}*/
}
