using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_STANDALONE_WIN
using XInputDotNetPure;


public class resourcePetal : MonoBehaviour
{

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    public GameObject flowerSpawned;
    public float timeToCollect = 3f;
    float currentTime = 0.0f;

    public int maxResources = 20;
    public int minResources = 50;

    int resources;

    bool isColliding = false;


    // Use this for initialization
    void Start()
    {
        resources = Random.Range(minResources, maxResources + 1);
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

        if (isColliding)
        {
            // put controller input here
            if (state.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= timeToCollect)
                {
                    GameObject localPlayer1 = GameObject.Find("localPlayer1");
                    localPlayer1.GetComponent<networkPlayerScript>().resourcesAdd = resources;
                    localPlayer1.GetComponent<networkPlayerScript>().updateResources = true;
                    flowerSpawned.GetComponent<flowerScript>().currentResources--;
					GameObject theText = Resources.Load("floatingText") as GameObject;
					theText.GetComponent<Text>().text = "$ " + resources.ToString();
					GameObject number = Instantiate(theText, transform.position, theText.transform.rotation);
					number.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    Destroy(this.gameObject);
                }
            }
            else if (state.Buttons.LeftShoulder == ButtonState.Released)
            {
                currentTime = 0.0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("true");
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("TRUUEUWUEURUWUEEUFUW");
            isColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isColliding = false;
            currentTime = 0.0f;
        }
    }
}
#endif

#if UNITY_STANDALONE_OSX
public class resourcePetal : MonoBehaviour
{

}
#endif