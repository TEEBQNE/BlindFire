using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOutsideEnemy : MonoBehaviour {
	public int health;
	int startHealth;
	private float startTIme;
	private float journeyLength;
	public float healthWidth;
	public float healthHeight;
	public float yOffset;

	public GameObject healthbar;
	public GameObject newHealthBar;

	float amplitudeX = 1.0f;
	float amplitudeY = 1.0f;
	float omegaX = 1.0f;
	float omegaY = 1.0f;
	float index;

	float RotateSpeed = 3f;
	float Radius = 1.5f;
	float angle;

	public float speedSmoother;

	private Vector3 velocity = Vector3.zero;

	private Vector3 startPos;
	public Vector3 newPos;
	Vector2 offset;
	float currentTime = 0;
	float circleTime = 2f;
	float eightTime = 6.4f;
	float maxTime = 1.5f;

	float speed = 0f;

	bool isTimerGoing = false;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		newPos = startPos;
		offset.x = 0.02f;
		offset.y = 0.02f;

		if (gameObject.name == "FlyingEnemy30000")
		{
			health = 4;
			startHealth = 4;
			speed = 0.004f;
		}
		else if (gameObject.name == "FlyingEnemy2_0000_Layer-9-copy")
		{
			health = 10;
			startHealth = 10;
			speed = 0.004f;
		}
		else if (gameObject.name == "FlyingEnemy1_0000_Layer-2")
		{
			health = 1;
			startHealth = 1;
			speed = 0.04f;
		}

		newHealthBar = Instantiate (healthbar, new Vector3 (100f, 100f, 0f), transform.rotation);
		newHealthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);

		newHealthBar.GetComponent<enemyHealthBar> ().objectToFollow = this.gameObject.transform;
		newHealthBar.GetComponent<enemyHealthBar> ().offset = yOffset;
		newHealthBar.GetComponent<enemyHealthBar> ().width = healthWidth;
		newHealthBar.GetComponent<enemyHealthBar> ().height = healthHeight;
		newHealthBar.GetComponent<enemyHealthBar> ().isSet = true;


		startTIme = Time.time;
		journeyLength = Vector3.Distance(transform.position, (GameObject.Find("Path").GetComponent<SpawnEnemy>().waypoints[GameObject.Find("Path").GetComponent<SpawnEnemy>().waypoints.Count - 1]).transform.position);
	}

	// Update is called once per frame
	void Update () {

		float distCovered = (Time.time - startTIme) * speed;

		float fracJourney = distCovered / journeyLength;

		if (gameObject.name == "FlyingEnemy30000")
		{
			amplitudeX = 2.0f;
			omegaY = 2.0f;
			index += Time.deltaTime;
			float x = amplitudeX * Mathf.Cos(omegaX * index);
			float y = amplitudeY * Mathf.Sin(omegaY * index);
			transform.position = transform.parent.transform.position + new Vector3(x, y, 0);
		}
		else if(gameObject.name == "FlyingEnemy2_0000_Layer-9-copy")
		{
			Vector3 newestPos = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
			angle += RotateSpeed * Time.deltaTime;

			offset = new Vector2(Mathf.Sin(angle) * Radius, Mathf.Cos(angle) * Radius);
			Vector3 newerPos = new Vector3(offset.x, offset.y, newPos.z);
			Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, (newestPos + newerPos), ref velocity, speedSmoother);
			gameObject.transform.LookAt2D((GameObject.Find("Path").GetComponent<SpawnEnemy>().waypoints[GameObject.Find("Path").GetComponent<SpawnEnemy>().waypoints.Count - 1]).transform.position);
			transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
		}
		else if(gameObject.name == "FlyingEnemy1_0000_Layer-2")
		{
			gameObject.transform.LookAt2D((GameObject.Find("Path").GetComponent<SpawnEnemy>().waypoints[GameObject.Find("Path").GetComponent<SpawnEnemy>().waypoints.Count - 1]).transform.position);
		}

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.layer == 0 && col.gameObject.name == "tempBase")
		{
			GameObject localPlayer1 = GameObject.Find("localPlayer1");
			localPlayer1.GetComponent<networkPlayerScript>().healthChange = health;
			localPlayer1.GetComponent<networkPlayerScript>().updateHealth = true;
			SpawnEnemy.instance.outsideEnemiesList.Remove(transform.parent.gameObject);
			newHealthBar.GetComponent<enemyHealthBar> ().objectToFollow = null;
			Destroy (newHealthBar);
			Destroy(transform.parent.gameObject);
		}
		if(col.gameObject.layer == 18)
		{
			Destroy(col.gameObject);
			health--;
			if (health <= 0)
			{
				GameObject localPlayer1 = GameObject.Find ("localPlayer1");
				SpawnEnemy.instance.outsideEnemiesList.Remove(transform.parent.gameObject);
				newHealthBar.GetComponent<enemyHealthBar> ().objectToFollow = null;
				Destroy(newHealthBar);
				Destroy(transform.parent.gameObject);
				localPlayer1.GetComponent<networkPlayerScript> ().resourcesAdd = 2;
				localPlayer1.GetComponent<networkPlayerScript> ().updateResources = true;
			}
		}
	}
}