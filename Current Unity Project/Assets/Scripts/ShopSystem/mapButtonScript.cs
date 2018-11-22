using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class mapButtonScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {

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

	public void openMap()
	{
		gameObject.GetComponent<Outline> ().enabled = false;
		activeMap.transform.Find ("enemyPath").gameObject.SetActive (true);

		GameManager.gameManager.GetComponent<GameManager> ().shopOpen = false;
		var turrets = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.tag == "turret");

		foreach(GameObject theTurrets in turrets)
		{
			theTurrets.SetActive (true);
		}

		theCanvas.transform.Find ("PlayButton").gameObject.SetActive (true);
		theCanvas.transform.Find ("shopButton").gameObject.SetActive (true);
		UIHolder.transform.Find ("shopPanel").gameObject.SetActive (false);
		UIHolder.transform.Find ("shopBackground").gameObject.SetActive (false);
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
