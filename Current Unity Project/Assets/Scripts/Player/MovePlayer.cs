using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public float playerSpeed;
    Collider2D playerColl;

	// Use this for initialization
	void Start () {
        playerColl = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector2.right * playerSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * playerSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector2.up * playerSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * playerSpeed);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerColl.isTrigger = true;
        }
        else
        {
            playerColl.isTrigger = false;
        }
    }
}
