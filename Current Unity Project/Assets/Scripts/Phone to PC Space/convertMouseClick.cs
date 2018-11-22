using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class convertMouseClick : MonoBehaviour {

	public float xRes;
	public float yRes;

	public float yMin;

	public GameObject circle;

	//GameObject mainCamera;


	// Use this for initialization
	void Start () {
		//mainCamera = GameObject.Find ("Main Camera");
		yMin = gameObject.transform.position.y - gameObject.GetComponent<SpriteRenderer> ().bounds.extents.y;
		yRes = 2f * gameObject.GetComponent<SpriteRenderer> ().bounds.extents.y;
		xRes = 2f * gameObject.GetComponent<SpriteRenderer> ().bounds.extents.x;
	}
	
	// Update is called once per frame
	void Update () {
		// This is done in the network script
		/*GameObject localPlayer2 = GameObject.Find ("localPlayer2");
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mouseClick = mainCamera.GetComponent<Camera> ().ScreenToWorldPoint (Input.mousePosition);
			if (mouseClick.y >= yMin && mouseClick.y <= (yMin + yRes) && mouseClick.x >= -xRes / 2f && mouseClick.x <= xRes) {
				GameObject temp;
				localPlayer2.GetComponent<networkPlayerScript> ().xPercent = ((mouseClick.x) / xRes);
				localPlayer2.GetComponent<networkPlayerScript> ().yPercent = ((mouseClick.y + yMin) / yRes);
				localPlayer2.GetComponent<networkPlayerScript> ().spawnCircle = true;
				temp = Instantiate (circle, new Vector2 (mouseClick.x, mouseClick.y), transform.rotation);
				temp.transform.localScale = new Vector3 (0.8f, 0.8f, temp.transform.localScale.z);
				Debug.Log ("CLICK" + "X: " + (mouseClick.x) / xRes + "Y: " + ((mouseClick.y + yMin) / yRes));
			}
		}*/
	}
}
