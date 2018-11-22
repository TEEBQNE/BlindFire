using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealthBar : MonoBehaviour {

	GameObject slider;

	public bool isSet = false;

	bool setHealth = false;

	public Transform objectToFollow;

	//Transform m_trackingTransform;
	Vector3 mousePos;

	public float offset = -0.1f;

	public float width = 30f;
	public float height = 10f;

	float startHealth;

	RectTransform _myCanvas;

	float currentHealth;

	// Use this for initialization
	void Start () {
		// Gameobjects
		_myCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
		slider = this.gameObject;
		slider.GetComponent<Slider>().GetComponent<RectTransform>().sizeDelta = new Vector2 (width, height);


		// slider colors
		slider.GetComponent<Slider> ().transform.Find ("Background").GetComponent<Image> ().color = new Color (122f / 255f, 2f / 255f, 2f / 255f, 1f);
		slider.GetComponent<Slider> ().transform.Find ("Fill Area").transform.Find ("Fill").GetComponent<Image> ().color = new Color (0f, 1f, 12f / 255f, 1f);
	}

	// Update is called once per frame
	void Update () {
		if (!setHealth) {
			setHealth = true;
			if (objectToFollow.gameObject.tag == "Enemy") {
				startHealth = objectToFollow.gameObject.GetComponent<MoveEnemy> ().Health;
			} else {
				startHealth = objectToFollow.gameObject.GetComponent<MoveOutsideEnemy> ().health;
			}
			currentHealth = startHealth;
		}
		try
		{
			if(objectToFollow.transform == null)
			{
				Destroy (this.gameObject);
			}
		}
		catch(MissingReferenceException) {
			Destroy (this.gameObject);
			objectToFollow = null;
			return;
		}

		try{
			if(objectToFollow.transform == null)
			{
				Destroy (this.gameObject);
			}
			if (objectToFollow.gameObject.tag == "Enemy") {
				currentHealth = objectToFollow.gameObject.GetComponent<MoveEnemy> ().Health;
			} else {
				currentHealth = objectToFollow.gameObject.GetComponent<MoveOutsideEnemy> ().health;
			}
			if (currentHealth <= 0) {
				Destroy (this.gameObject);
			}
			slider.GetComponent<Slider> ().value = currentHealth / startHealth;

		}catch(MissingReferenceException) {
			Destroy (this.gameObject);
			objectToFollow = null;
			return;
		}

	}

	void LateUpdate()
	{
		Vector3 localOffset = new Vector3 (0f, offset, 0f);
		// Translate our anchored position into world space.
		Vector3 worldPoint = objectToFollow.TransformPoint(localOffset);

		// Translate the world position into viewport space.
		Vector3 viewportPoint = Camera.main.WorldToViewportPoint(worldPoint);

		// Canvas local coordinates are relative to its center, 
		// so we offset by half. We also discard the depth.
		viewportPoint -= 0.5f * Vector3.one; 
		viewportPoint.z = 0;

		// Scale our position by the canvas size, 
		// so we line up regardless of resolution & canvas scaling.
		Rect rect = _myCanvas.rect;
		viewportPoint.x *= rect.width;
		viewportPoint.y *= rect.height;

		// Add the canvas space offset and apply the new position.
		transform.localPosition = viewportPoint;
	}
}
