using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animDestroyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void destroy()
	{
		Destroy (this.gameObject);
	}

	public void destroyBomb()
	{
		Instantiate(Resources.Load("bombExplosion"), new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
		Destroy(this.gameObject);
	}
}
