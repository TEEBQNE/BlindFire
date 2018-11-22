using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class copyTextScript : MonoBehaviour {

	public GameObject textToCopy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Text> ().text = textToCopy.GetComponent<Text> ().text;
	}
}
