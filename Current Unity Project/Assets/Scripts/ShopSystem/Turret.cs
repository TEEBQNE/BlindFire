using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Turret {

	public string turretName;
	public int turretID;

	public string lockedSprite;
	public string unlockedSprite;

	public float turretPrice;

	public string turretDescription;

	public bool unlocked;

	// if more info is needed pass it here
}
