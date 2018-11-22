using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerScript : MonoBehaviour {

	public Vector3 flower1;
	/*public Vector3 flower2;
	public Vector3 flower3;
	public Vector3 flower4;*/

	public GameObject flower;
	public int currentResources = 1;

	GameObject petal1;
	/*GameObject petal2;
	GameObject petal3;
	GameObject petal4;*/

	bool hasSpawned = false;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	#if UNITY_STANDALONE_WIN
	void Update () {
		if (!AnimatorIsPlaying () && !hasSpawned) {
			hasSpawned = true;
			petal1 = Instantiate (flower, new Vector3 (transform.position.x + flower1.x, transform.position.y + flower1.y, flower.transform.position.z), transform.rotation);
			petal1.GetComponent<resourcePetal> ().flowerSpawned = this.gameObject;
			/*petal2 = Instantiate (flower, new Vector3 (transform.position.x + flower2.x, transform.position.y + flower2.y, flower.transform.position.z), transform.rotation);
			petal2.GetComponent<resourcePetal> ().flowerSpawned = this.gameObject;
			petal3 = Instantiate (flower, new Vector3 (transform.position.x + flower3.x, transform.position.y + flower3.y, flower.transform.position.z), transform.rotation);
			petal3.GetComponent<resourcePetal> ().flowerSpawned = this.gameObject;
			petal4 = Instantiate (flower, new Vector3 (transform.position.x + flower4.x, transform.position.y + flower4.y, flower.transform.position.z), transform.rotation);
			petal4.GetComponent<resourcePetal> ().flowerSpawned = this.gameObject;
			*/
		}


		if (hasSpawned) {
			if (GameManager.gameManager.GetComponent<GameManager> ().waveOngoing) {
				if (petal1 != null) {
					petal1.gameObject.SetActive (true);
				}

				/*if (petal2 != null) {
					petal2.gameObject.SetActive (true);
				}

				if (petal3 != null) {
					petal3.gameObject.SetActive (true);
				}

				if (petal4 != null) {
					petal4.gameObject.SetActive (true);
				}*/
			} else {
				if (petal1 != null) {
					petal1.gameObject.SetActive (false);
				}

				/*if (petal2 != null) {
					petal2.gameObject.SetActive (false);
				}

				if (petal3 != null) {
					petal3.gameObject.SetActive (false);
				}

				if (petal4 != null) {
					petal4.gameObject.SetActive (false);
				}*/
			}
		}

		if (currentResources == 0) {
			GameManager.gameManager.GetComponent<GameManager> ().currentResourceFlowers--;
			Destroy (gameObject);
		}
	}
	#endif

	bool AnimatorIsPlaying(){
		return gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <
			0.6f;
	}
}
