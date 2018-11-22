using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Bullet
{
    public GameObject bulletPref;
    public float shootInterval = 0.75f;
}

public class ShootScript : MonoBehaviour
{
	GameObject gameTarget ;
	public List<GameObject> spawnPoints = new List<GameObject>();
    public Bullet bullet;
    public List<GameObject> enemiesInRange;
    private float timeLastShot;
	public float rotateSpeed = 10f;
	float radius;
	public int kills = 0;
	public int damage = 1;
	public Sprite bulletSprite;

	public GameObject explosionPrefab;
	public GameObject rocketPrefab;

	public int turretID = 0;

	public float bulletSpeed;

	public string target = "close";

	// draws vision in editor
	/*void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color (211f/255f, 211f/255f, 211f/255f, 180f/255f);
		Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y, 0f), gameObject.GetComponent<CircleCollider2D>().radius);
	}*/

    // Use this for initialization
    void Start()
    {
		//target = "close";
		//radius = gameObject.GetComponent<CircleCollider2D> ().radius;
        enemiesInRange = new List<GameObject>();
        timeLastShot = Time.time;
		gameTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
		if (gameObject.name == "Rocket Turret(Clone)") {
			if (GameManager.gameManager.GetComponent<GameManager> ().waveOngoing && SceneManager.GetActiveScene().name == "GameScene") {
				if (Time.time - timeLastShot > bullet.shootInterval) {
					GameObject explosion;
					Instantiate (rocketPrefab, transform.position, transform.rotation);
					explosion = Instantiate (explosionPrefab, transform.Find ("ViewField").transform.position, transform.rotation);
					explosion.GetComponent<rocketExplosionScript>().damage = damage;
					explosion.GetComponent<rocketExplosionScript>().turretFired = this.gameObject;
					timeLastShot = Time.time;
				}
			}
		} else {
			gameTarget = null;
			radius = gameObject.transform.Find ("visionCollider").GetComponent<CircleCollider2D> ().radius;
			// removes all empty game objects from list
			// for some reason they are not getting removed?
			enemiesInRange.RemoveAll (GameObject => GameObject == null);
			//GameObject gameTarget = null;
			// change this depending if it is first, last, closest or most health target


			if (gameObject.name == "Glue Turret(Clone)") {
				foreach (GameObject enemy in enemiesInRange) {
					if (enemy != null) {
					
						if (gameObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 0) {
							enemy.GetComponent<MoveEnemy> ().newSpeed = enemy.GetComponent<MoveEnemy> ().slowSpeed1;
						} else if (gameObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 1) {
							enemy.GetComponent<MoveEnemy> ().newSpeed = enemy.GetComponent<MoveEnemy> ().slowSpeed2;
						} else if (gameObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 2) {
							enemy.GetComponent<MoveEnemy> ().newSpeed = enemy.GetComponent<MoveEnemy> ().slowSpeed3;
						} else if (gameObject.GetComponent<turretUpgradeScript> ().currentUpgradeLevel1 == 3) {
							enemy.GetComponent<MoveEnemy> ().newSpeed = enemy.GetComponent<MoveEnemy> ().slowSpeed4;
						}
					}
				}
			}

			if (target == "close") {
				Debug.Log ("CLOSE");
				if (gameTarget == null || gameTarget.layer == 21) {
					float minDistance = float.MaxValue;

					foreach (GameObject enemy in enemiesInRange) {
						if (enemy != null) {
							if (gameTarget != null) {
								Debug.Log (enemy.name);
							}
							float distanceBetween = Vector2.Distance (transform.position, enemy.transform.position);
							if (distanceBetween < minDistance && distanceBetween < radius) {
								gameTarget = enemy;
								minDistance = distanceBetween;
							}
						}
					}
					if (gameTarget != null) {
						if (Time.time - timeLastShot > bullet.shootInterval) {
							if (gameObject.name == "Sniper Turret(Clone)") {
								// this is set in the turnSniper
							} else {
								Vector3 difference = gameTarget.transform.position - transform.Find ("turretGun").position;
								float rotationZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;

								if (Mathf.Abs (transform.Find ("turretGun").transform.rotation.z - (rotationZ - 90f)) >= 180f) {
									transform.Find ("turretGun").transform.rotation = Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f);
								} else {
									transform.Find ("turretGun").transform.rotation = Quaternion.Slerp (transform.Find ("turretGun").transform.rotation, Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f), 1000000f);
								}	
							}
							Shoot (gameTarget.GetComponent<Collider2D> ());
							timeLastShot = Time.time;
						}


					}
				}
			}

			if (target == "first") {
				if (gameTarget == null || gameTarget.layer == 21) {
					foreach (GameObject enemy in enemiesInRange) {
						if (enemy != null) {
							gameTarget = enemiesInRange [0];
						}
					}
					if (gameTarget != null) {
						if (Time.time - timeLastShot > bullet.shootInterval) {
							if (gameObject.name == "Sniper Turret(Clone)") {
								// this is set in the turnSniper
							} else {
								Vector3 difference = gameTarget.transform.position - transform.Find ("turretGun").position;
								float rotationZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;

								if (Mathf.Abs (transform.Find ("turretGun").transform.rotation.z - (rotationZ - 90f)) >= 180f) {
									transform.Find ("turretGun").transform.rotation = Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f);
								} else {
									transform.Find ("turretGun").transform.rotation = Quaternion.Slerp (transform.Find ("turretGun").transform.rotation, Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f), 1000000f);
								}
							}
							Shoot (gameTarget.GetComponent<Collider2D> ());
							timeLastShot = Time.time;
						}
					}
				}
			}

			if (target == "last") {
				if (gameTarget == null || gameTarget.layer == 21) {
					foreach (GameObject enemy in enemiesInRange) {
						if (enemy != null) {
							gameTarget = enemiesInRange [enemiesInRange.Count - 1];
						}
					}
					if (gameTarget != null) {
						if (Time.time - timeLastShot > bullet.shootInterval) {
							if (gameObject.name == "Sniper Turret(Clone)") {
								// this is set in the turnSniper
							} else {
								Vector3 difference = gameTarget.transform.position - transform.Find ("turretGun").position;
								float rotationZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;

								if (Mathf.Abs (transform.Find ("turretGun").transform.rotation.z - (rotationZ - 90f)) >= 180f) {
									transform.Find ("turretGun").transform.rotation = Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f);
								} else {
									transform.Find ("turretGun").transform.rotation = Quaternion.Slerp (transform.Find ("turretGun").transform.rotation, Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f), 1000000f);
								}
							}
							Shoot (gameTarget.GetComponent<Collider2D> ());
							timeLastShot = Time.time;


							/*if (Mathf.Abs (transform.Find ("turretGun").transform.rotation.z - (rotationZ - 90f)) >= 120f) {
							
						}*/
							/*} else {
							transform.Find ("turretGun").transform.rotation = Quaternion.Slerp (transform.Find ("turretGun").transform.rotation, Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f), rotateSpeed * Time.deltaTime);
						}*/
						}

			

					}

				}
			}

			if (target == "strong") {
				if (gameTarget == null || gameTarget.layer == 21) {
					float maxHealth = 0;

					foreach (GameObject enemy in enemiesInRange) {
						if (enemy != null) {
							if (enemy.GetComponent<MoveEnemy> ().health > maxHealth) {
								maxHealth = enemy.GetComponent<MoveEnemy> ().health;
								gameTarget = enemy;
							}
						}
					}
					if (gameTarget != null) {
						if (Time.time - timeLastShot > bullet.shootInterval) {
							if (gameObject.name == "Sniper Turret(Clone)") {
								// this is set in the turnSniper
							} else {
								Vector3 difference = gameTarget.transform.position - transform.Find ("turretGun").position;
								float rotationZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;

								if (Mathf.Abs (transform.Find ("turretGun").transform.rotation.z - (rotationZ - 90f)) >= 180f) {
									transform.Find ("turretGun").transform.rotation = Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f);
								} else {
									transform.Find ("turretGun").transform.rotation = Quaternion.Slerp (transform.Find ("turretGun").transform.rotation, Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f), 1000000f);
								}
							}
							Shoot (gameTarget.GetComponent<Collider2D> ());
							timeLastShot = Time.time;
						}
					}
				}
			}
		}
    }

    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
		if (coll.gameObject.tag.Equals("Enemy") || coll.gameObject.layer == 21)
        {
            enemiesInRange.Add(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
		if (coll.gameObject.tag.Equals("Enemy"))
		{
			if (gameObject.name == "Glue Turret(Clone)") {
				coll.gameObject.GetComponent<MoveEnemy> ().newSpeed = coll.gameObject.GetComponent<MoveEnemy> ().normalSpeed;
			}
			//Debug.Break ();
			enemiesInRange.Remove(coll.gameObject);
		}
    }

	void Shoot(Collider2D targetCollider)
    {
		Debug.Log ("FIRED");
		//Vector3 difference = gameTarget.transform.position - transform.Find ("turretGun").position;
		//float rotationZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		//transform.Find ("turretGun").transform.rotation = (Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f));
		for (int x = 0; x < spawnPoints.Count; x++) {
			//Debug.Log ("FIRING");
			Vector3 startPos = gameObject.transform.position;
			Vector3 targetPos = targetCollider.transform.position;
			GameObject newBullet = Instantiate (bullet.bulletPref.gameObject, spawnPoints[x].transform.position, spawnPoints[x].transform.rotation);
			newBullet.transform.position = startPos;
			BulletScript bulletComp = newBullet.GetComponent<BulletScript> ();
			bulletComp.spriteToSet = bulletSprite;
			bulletComp.target = targetCollider.gameObject;
			bulletComp.startPos = startPos;
			if (gameObject.name == "Shotgun Turret(Clone)") {
				//bulletComp.transform.rotation = spawnPoints [x].transform.rotation;
				bulletComp.angleToFire = spawnPoints [x].transform;
			} else {
				bulletComp.transform.rotation = gameObject.transform.Find ("turretGun").transform.rotation;
			}
			bulletComp.targetPos = targetPos;
			bulletComp.damage = damage;
			bulletComp.targetObject = targetCollider.gameObject;
			bulletComp.turretFired = this.gameObject;
			bulletComp.bulletSpeed = bulletSpeed;
			bulletComp.angle = gameObject.transform.Find ("turretGun").eulerAngles.z;

			gameTarget = null;
		}
    }
}