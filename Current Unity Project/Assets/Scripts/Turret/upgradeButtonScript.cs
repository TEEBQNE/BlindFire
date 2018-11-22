using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class upgradeButtonScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {

	public GameObject currentTurret;

	float timeToBuy = 0.15f;
	public float currenTime = 0.0f;
	public bool startTimer = false;

	public Button activeButton;
	bool isOver = false;

	// Use this for initialization
	void Start () {

	}

	void OnEnable()
	{
		isOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (startTimer) {
			currenTime += Time.deltaTime;
		}
		if (activeButton != null) {
			if (activeButton.name == gameObject.name) {
				gameObject.GetComponent<Outline> ().enabled = true;
			}else if (!isOver) {
				gameObject.GetComponent<Outline> ().enabled = false;
			}
		}
	}

	public void firstButtonClick()
	{
		Debug.Log ("first");
		upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().firstPressed = true;
	}

	public void lastButtonClick()
	{
		Debug.Log ("last");
		upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().lastPressed = true;
	}

	public void strongButtonClick()
	{
		Debug.Log ("strong");
		upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().strongPressed = true;
	}

	public void closeButtonClick()
	{
		Debug.Log ("close");
		upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().closePressed = true;
	}

	public void sellButtonClick()
	{
		Debug.Log ("sell");
		upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().sellPressed = true;
	}

	public void buyUpgradeOneClick()
	{
		GameObject upgrade2 = GameObject.Find ("Canvas/UIHolder/upgradePanel/rightPart/upgradeTwo/upgradeButton2");
		upgrade2.GetComponent<upgradeButtonScript> ().startTimer = false;
		upgrade2.GetComponent<upgradeButtonScript> ().currenTime = 0.0f;
		if (currenTime > timeToBuy) {
		} else {
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().upgrade1Pressed = true;
		}
		startTimer = false;
		currenTime = 0.0f;
		Debug.Log ("1");
	}

	public void buyUpgradeTwoClick()
	{
		GameObject upgrade1 = GameObject.Find ("Canvas/UIHolder/upgradePanel/rightPart/upgradeOne/upgradeButton1");
		upgrade1.GetComponent<upgradeButtonScript> ().startTimer = false;
		upgrade1.GetComponent<upgradeButtonScript> ().currenTime = 0.0f;

		if (currenTime > timeToBuy) {
		} else {
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().upgrade2Pressed = true;
		}
		startTimer = false;
		currenTime = 0.0f;
		Debug.Log ("2");
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		startTimer = true;
		if (gameObject != activeButton) {
			gameObject.GetComponent<Outline> ().enabled = true;
			isOver = true;
		}

		if (gameObject.name.CompareTo ("upgradeButton1") == 0) {
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().upgrade1Hover = true;
		} else if(gameObject.name.CompareTo("upgradeButton2") == 0)
		{
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().upgrade2Hover = true	;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	if (gameObject != activeButton) {
			gameObject.GetComponent<Outline> ().enabled = false;
			
		}
			isOver = false;
		if (gameObject.name.CompareTo ("upgradeButton1") == 0) {
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().upgrade1Hover = false;
		} else if(gameObject.name.CompareTo("upgradeButton2") == 0)
		{
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().upgrade2Hover = false;
		}
	}
}