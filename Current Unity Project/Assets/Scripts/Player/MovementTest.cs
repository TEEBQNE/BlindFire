using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_STANDALONE_WIN
using XInputDotNetPure;

public class MovementTest : MonoBehaviour {

bool playerIndexSet = false;
PlayerIndex playerIndex;
GamePadState state;
GamePadState prevState;
Vector3 lookVector;
public Rigidbody2D rb;
float speed = .13f;
public int health = 100;

// Use this for initialization
void Start () {
rb = GetComponent<Rigidbody2D>();
}

// Update is called once per frame
void Update () {
/*if(health <= 0f)
{
Destroy(gameObject);
}*/

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

//transform.Translate(Vector2.up * state.ThumbSticks.Left.Y * speed);
//transform.Translate(Vector2.right * state.ThumbSticks.Left.X * speed);

rb.MovePosition(new Vector2((transform.position.x + state.ThumbSticks.Left.X * speed), transform.position.y + state.ThumbSticks.Left.Y * speed));

if (prevState.Triggers.Left < 1 && state.Triggers.Left == 1)
{
Dash();
speed = 0.13f;
}
}

void Dash()
{
speed = 1.5f;
transform.Translate(Vector2.up * state.ThumbSticks.Left.Y * speed);
transform.Translate(Vector2.right * state.ThumbSticks.Left.X * speed);
Debug.Log(speed);
}
}
#endif

#if UNITY_ANDROID
public class MovementTest : MonoBehaviour
{
public int health = 100;

void Update () {
if(health <= 0f)
{
Destroy(gameObject);
}
}
}
#endif


#if UNITY_STANDALONE_OSX
public class MovementTest : MonoBehaviour
{
	public int health = 100;

	void Update () {
		/*if(health <= 0f)
		{
			Destroy(gameObject);
		}*/
	}
}
#endif