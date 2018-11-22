using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToPosition : MonoBehaviour {

	Transform toLocation;

	void OnEnable()
	{
		toLocation = GameObject.Find ("Canvas/UIHolder/shopPanel/itemInfoLocation/").transform;
		transform.position = toLocation.position;
	}
}
