using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    // Trigger for game over
    private void OnCollisionEnter2D(Collision2D collision) {
        // Checks if drivers head or front of the car runs into map (terrain)
        if(collision.gameObject.CompareTag("Map")) {
            //  and if yes, end the game
            GameManager.instance.GameOver();
        }
    }
}
