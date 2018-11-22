using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_STANDALONE_WIN
using XInputDotNetPure;

public class RotTest : MonoBehaviour
{

bool playerIndexSet = false;
PlayerIndex playerIndex;
GamePadState state;
GamePadState prevState;
Vector3 lookVector;
float x;
float y;

float maxTime = 0.2f;
private float timeLastShot;

public GameObject bulletPrefab;
public Transform bulletSpawn1;
public Transform bulletSpawn2;

// Use this for initialization
void Start()
{
// should fix the cross OS null references
bulletSpawn1 = gameObject.transform.GetChild(1);
bulletSpawn2 = gameObject.transform.GetChild(2);
bulletPrefab = Resources.Load("playerBullet") as GameObject;
timeLastShot = Time.time;
}

void FixedUpdate()
{
GamePad.SetVibration(playerIndex, 0f, state.Triggers.Right * .5f);
}

// Update is called once per frame
void Update()
{

// Find a PlayerIndex, for a single player game
// Will find the first controller that is connected ans use it
if (!playerIndexSet || !prevState.IsConnected)
{
for (int i = 0; i < 4; ++i)
{
PlayerIndex testPlayerIndex = (PlayerIndex)i;
GamePadState testState = GamePad.GetState(testPlayerIndex);
if (testState.IsConnected)
{
Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
playerIndex = testPlayerIndex;
playerIndexSet = true;
}
}
}

prevState = state;
state = GamePad.GetState(playerIndex);

x = state.ThumbSticks.Right.X;
y = state.ThumbSticks.Right.Y;
lookVector = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan2(x, y) * 180 / Mathf.PI * -1);

if (lookVector.sqrMagnitude > 0.1f)
{
transform.eulerAngles = lookVector;
}

if (state.Triggers.Right == 1)
{
if(Time.time - timeLastShot > maxTime){
Fire();
timeLastShot = Time.time;
}
}

if (prevState.Triggers.Right < 1 && state.Triggers.Right == 1)
{
Fire();
}
}

void Fire()
{
var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn1.position, bulletSpawn1.rotation);
var bullet2 = (GameObject)Instantiate(bulletPrefab, bulletSpawn2.position, bulletSpawn2.rotation);

bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * 20;
bullet2.GetComponent<Rigidbody2D>().velocity = bullet2.transform.up * 20;

Destroy(bullet, 2.0f);
}
}
#endif

#if UNITY_STANDALONE_OSX
public class RotTest : MonoBehaviour
{

}
#endif

#if UNITY_ANDROID
public class RotTest : MonoBehaviour
{

}
#endif