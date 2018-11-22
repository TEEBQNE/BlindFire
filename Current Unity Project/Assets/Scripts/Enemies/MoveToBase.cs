using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBase : MonoBehaviour {

	private float startTIme;
	private float journeyLength;
	private Vector3 startPos;
	public Vector3 newPos;
	float speed = 0.004f;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		newPos = startPos;
		startTIme = Time.time;
		journeyLength = Vector3.Distance(transform.position, (GameObject.Find("Path").GetComponent<SpawnEnemy>().waypoints[GameObject.Find("Path").GetComponent<SpawnEnemy>().waypoints.Count - 1]).transform.position);
	}

	// Update is called once per frame
	void Update () {

		if (gameObject.name == "FlyEnemyMove(Clone)")
		{
			speed = 0.004f;
		}
		else if (gameObject.name == "RayEnemyMove(Clone)")
		{
			speed = 0.004f;
		}
		else if (gameObject.name == "WingEnemyMove(Clone)")
		{
			speed = 0.1f;
		}

		float distCovered = (Time.time - startTIme) * speed;

		float fracJourney = distCovered / journeyLength;

		newPos = Vector3.Lerp(newPos, (GameObject.Find("Path").GetComponent<SpawnEnemy>().waypoints[GameObject.Find("Path").GetComponent<SpawnEnemy>().waypoints.Count - 1]).transform.position, fracJourney);
		transform.position = newPos;
	}
}
