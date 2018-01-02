using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour {

    public GameObject platform;
    public GameObject[] startPlatforms;
    public List<GameObject> objectsInScene = new List<GameObject>();
    public List<GameObject> potionsInScene = new List<GameObject>();
    public GameObject gameEnd;
    public GameObject playerInfo;
    public GameObject potion;
    public Text winText;

    private void Awake()
    {
        foreach (GameObject p in startPlatforms)
        {
            objectsInScene.Add(p);
        }
    }
    // Use this for initialization
    void Update() {
        if (objectsInScene.Count < 14)
        {
            platform.transform.position = new Vector3(15, Random.Range(3.0f, -3.0f), -1);
            objectsInScene.Add(Instantiate(platform));
            if (potionsInScene.Count < 1)
            {
                platform.transform.position += new Vector3(0, 0.5f, 0);
                potionsInScene.Add(Instantiate(potion, platform.transform.position, new Quaternion()));
            }
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        foreach (GameObject g in objectsInScene)
            g.transform.position += new Vector3(-0.05f,0,0);
        foreach(GameObject g in potionsInScene)
            g.transform.position += new Vector3(-0.05f, 0, 0);
    }

    public void Player1Wins()
    {
        winText.text = "Player 1 Wins";
        playerInfo.SetActive(false);
        gameEnd.SetActive(true);
    }

    public void Player2Wins()
    {
        winText.text = "Player 2 Wins";
        playerInfo.SetActive(false);
        gameEnd.SetActive(true);
    }
}
