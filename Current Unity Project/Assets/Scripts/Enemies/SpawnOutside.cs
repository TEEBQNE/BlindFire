using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOutside : MonoBehaviour {
    public static SpawnOutside instance;
    public List<GameObject> spawnPoints;
    public GameObject outsideEnemy;
    public float outsideSpawnInterval = 1;

    public List<GameObject> outsideEnemiesList;

    private int outsideEnemiesSpawned;

    // Use this for initialization
    void Start () {
        instance = this;

        outsideEnemiesList = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        /*int randNum = Random.Range(0, 3);
        Vector3 spawnPoint = new Vector3(0, 0, 0);
        if(randNum == 0)
        {
            spawnPoint = new Vector3(-5.93f, 5.57f, 0);
        }
        else if (randNum == 1)
        {
            spawnPoint = new Vector3(6.98f, 3.84f, 0);
        }
        else if (randNum == 2)
        {
            spawnPoint = new Vector3(-8.34f, -3.7f, 0);
        }

        float timeInterval = Time.time - lastSpawnTime;

        if (outsideEnemiesSpawned < 8 && timeInterval > outsideSpawnInterval)
        {
            lastSpawnTime = Time.time;
            GameObject newOutsideEnemy = Instantiate(outsideEnemy, spawnPoint, Quaternion.identity);
            //move it towards base here
            outsideEnemiesList.Add(newOutsideEnemy);
            outsideEnemiesSpawned++;
        }
        */
	}
}
