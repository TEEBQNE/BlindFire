using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

	public List<GameObject> waypoints = new List<GameObject>();
    private int currentWaypoint = 0;
    private float timeSinceLastWaypoint;
    public float speed = .0f;
	public int health = 0;
	public int resourceAdd = 0;
	public float normalSpeed;
	public float slowSpeed1;
	public float slowSpeed2;
	public float slowSpeed3;
	public float slowSpeed4;
	public float newSpeed = 0f;

	public float healthWidth;
	public float healthHeight;
	public float yOffset;

	public GameObject healthbar;
	public GameObject newHealthBar;


	public string deathAnim = "";

	// Use this for initialization
	void Start () {
        timeSinceLastWaypoint = Time.time;
		newSpeed = speed;
		newHealthBar = Instantiate (healthbar, new Vector3 (100f, 100f, 0f), transform.rotation);
		newHealthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);

		newHealthBar.GetComponent<enemyHealthBar> ().objectToFollow = this.gameObject.transform;
		newHealthBar.GetComponent<enemyHealthBar> ().offset = yOffset;
		newHealthBar.GetComponent<enemyHealthBar> ().width = healthWidth;
		newHealthBar.GetComponent<enemyHealthBar> ().height = healthHeight;
		newHealthBar.GetComponent<enemyHealthBar> ().isSet = true;
	}
	
	// Update is called once per frame
	void Update ()
	{

		GameObject localPlayer1;
		localPlayer1 = GameObject.Find ("localPlayer1");
        Vector3 startPos = waypoints[currentWaypoint].transform.position;
        Vector3 endPos = waypoints[currentWaypoint + 1].transform.position;

        float pathLength = Vector3.Distance(startPos, endPos);
		float totalPathTime;
		if (newSpeed != speed) {
			speed = Mathf.Lerp (speed, newSpeed, Time.deltaTime * 2.5f);
			totalPathTime = pathLength / speed;
		} else {
			totalPathTime = pathLength / speed;
		}
        float currentPathTime = Time.time - timeSinceLastWaypoint;
        gameObject.transform.position = Vector2.Lerp(startPos, endPos, currentPathTime / totalPathTime);

        if (gameObject.transform.position.Equals(endPos))
        {
			if(currentWaypoint < waypoints.Count - 2)
            {
                currentWaypoint++;
                timeSinceLastWaypoint = Time.time;
                RotateEnemy();
            }
            else
            {
                Destroy(gameObject);
				Destroy (newHealthBar);
                if (gameObject.name == "droneEnemy(Clone)")
                {
                    SpawnEnemy.instance.droneEnemiesList.Remove(gameObject);
                }
                if (gameObject.name == "tankEnemy(Clone)")
                {
                    SpawnEnemy.instance.tankEnemiesList.Remove(gameObject);
                }
                if (gameObject.name == "spiderEnemy(Clone)")
                {
                    SpawnEnemy.instance.spiderEnemiesList.Remove(gameObject);
                }
				localPlayer1.GetComponent<networkPlayerScript> ().healthChange = health;
				localPlayer1.GetComponent<networkPlayerScript> ().updateHealth = true;
            }
        }
    }

	public int Health
	{
		get{
			return health;
		}
		set {
			health = value;
		}
	}

    private void RotateEnemy()
    {
        Vector3 newStartPos = waypoints[currentWaypoint].transform.position;
        Vector3 newEndPos = waypoints[currentWaypoint + 1].transform.position;
        Vector3 newDirection = newEndPos - newStartPos;

        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
        
        GameObject droneSprite = gameObject.transform.Find("enemySprite").gameObject;
        droneSprite.transform.rotation = Quaternion.AngleAxis(rotationAngle-90f, Vector3.forward);
    }

    public float DistanceToEnd()
    {
        float distance = 0;
        distance += Vector2.Distance(gameObject.transform.position, waypoints[currentWaypoint + 1].transform.position);

        for (int i = currentWaypoint + 1; i < waypoints.Count - 1; i++)
        {
            Vector3 startPos = waypoints[i].transform.position;
            Vector3 endPos = waypoints[i + 1].transform.position;
            distance += Vector2.Distance(startPos, endPos);
        }
        return distance;
    }
}
