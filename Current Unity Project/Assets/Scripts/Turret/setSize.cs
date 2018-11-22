using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setSize : MonoBehaviour {

	public GameObject pivot1;
	public GameObject pivot2;
	public GameObject pivot3;
	public GameObject pivot4;

	GameObject sniperP1;
	GameObject sniperP2;
	GameObject sniperP3;
	GameObject sniperP4;

	public GameObject rocketTarget;

	GameObject rocketT;

	// Use this for initialization
	void Start () {
		if (transform.parent.name == "Sniper Turret(Clone)") {
			sniperP1 = Instantiate (pivot1, transform.position, transform.rotation);
			sniperP1.GetComponent<followTurret> ().toFollow = transform.parent.gameObject;

			sniperP2 = Instantiate (pivot2, transform.position, transform.rotation);
			sniperP2.GetComponent<followTurret> ().toFollow = transform.parent.gameObject;

			sniperP3 = Instantiate (pivot3, transform.position, transform.rotation);
			sniperP3.GetComponent<followTurret> ().toFollow = transform.parent.gameObject;

			sniperP4 = Instantiate (pivot4, transform.position, transform.rotation);
			sniperP4.GetComponent<followTurret> ().toFollow = transform.parent.gameObject;

		} else if (transform.parent.name == "Rocket Turret(Clone)") {
			gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
			rocketT = Instantiate (rocketTarget, transform.position, transform.rotation);
			rocketT.GetComponent<followTurret> ().toFollow = transform.parent.gameObject;
		}
	}

	// Update is called once per frame
	void Update () {
		// make sure that the scale of the sprite starts out as 1x 1x
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			transform.localScale = new Vector3 (transform.parent.transform.Find ("visionCollider").GetComponent<CircleCollider2D> ().transform.localScale.x * 2f * transform.parent.transform.Find ("visionCollider").GetComponent<CircleCollider2D> ().bounds.extents.x, transform.parent.transform.Find ("visionCollider").GetComponent<CircleCollider2D> ().transform.localScale.y * 2f * transform.parent.transform.Find ("visionCollider").GetComponent<CircleCollider2D> ().bounds.extents.y, 1f);
		} else if (SceneManager.GetActiveScene ().name == "UIScene") {
			transform.localScale = new Vector3 (transform.parent.transform.Find ("visionCollider").GetComponent<CircleCollider2D> ().transform.localScale.x * 1.5f * 2f * transform.parent.transform.Find ("visionCollider").GetComponent<CircleCollider2D> ().bounds.extents.x, transform.parent.transform.Find ("visionCollider").GetComponent<CircleCollider2D> ().transform.localScale.y * 1.5f * 2f * transform.parent.transform.Find ("visionCollider").GetComponent<CircleCollider2D> ().bounds.extents.y, 1f);
			if (transform.parent.name == "Sniper Turret(Clone)") {
				//if (GameManager.gameManager.GetComponent<GameManager> ().waveOngoing == false) {
				sniperP1.SetActive (true);
				sniperP2.SetActive (true);
				sniperP3.SetActive (true);
				sniperP4.SetActive (true);
				/*} else {
					pivot1.SetActive (false);
					pivot2.SetActive (false);
					pivot3.SetActive (false);
					pivot4.SetActive (false);
				}*/
			} else if (transform.parent.name == "Rocket Turret(Clone)") {
				gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
				rocketT.SetActive (true);
			}
		}
	}

	public void deleteAll()
	{
		if (transform.parent.name == "Sniper Turret(Clone)") {
			Destroy (sniperP1);
			Destroy (sniperP2);
			Destroy (sniperP3);
			Destroy (sniperP4);

		} else if (transform.parent.name == "Rocket Turret(Clone)") {
			Destroy (rocketT);
		}
	}
}