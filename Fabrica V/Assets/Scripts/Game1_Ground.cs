using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1_Ground : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;

    public bool isGround;

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGround = collision != null && (((1 << collision.gameObject.layer) & groundMask) != 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGround = false;
    }
}
