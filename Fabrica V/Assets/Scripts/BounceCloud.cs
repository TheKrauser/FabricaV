using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceCloud : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Feet"))
        {
            var character = collision.collider.GetComponentInParent<Game1_Character>();
            character.Bounce();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Feet"))
        {
            var character = collision.GetComponentInParent<Game1_Character>();
            character.Bounce();
        }
    }
}
