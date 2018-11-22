using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollectScript : MonoBehaviour {

    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        float currentResources = gameManager.Resource;
	}
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("Player") && Input.GetKey(KeyCode.Space))
        {
            gameManager.Resource += .025f;
        }
    }
}
