using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelRefill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2d) {
        // If player touches the can, refill the fuel level
        if(collider2d.gameObject.CompareTag("Player")) {
            FuelController.instance.Refill();
            Destroy(gameObject);
        }
    }
}
