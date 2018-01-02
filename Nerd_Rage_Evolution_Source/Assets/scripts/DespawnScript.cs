using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnScript : MonoBehaviour {

    public GameObject controller;

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameControlScript control = controller.GetComponent<GameControlScript>();

        if (coll.gameObject.tag != "Player")
        {
            control.objectsInScene.Remove(coll.gameObject);
            Destroy(coll.gameObject);
        }
        else
        {
            if (coll.gameObject.name == "player1")
            {
                Destroy(coll.gameObject);
                Destroy(GameObject.Find("player2"));
                control.Player2Wins();
            }
            else
            {
                Destroy(coll.gameObject);
                Destroy(GameObject.Find("player1"));
                control.Player1Wins();
            }
        }
    }


}
