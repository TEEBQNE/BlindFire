using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class turretUnlockButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Outline> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		gameObject.GetComponent<Outline> ().enabled = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		gameObject.GetComponent<Outline> ().enabled = false;
	}

	public void shopButton()
	{
		transform.parent.gameObject.SetActive (false);
	}

	public void closeButton()
	{
		transform.parent.gameObject.SetActive (false);
	}
}	
