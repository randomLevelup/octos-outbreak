using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleJumpIsGrounded : MonoBehaviour
{
    public int numWallsTouching = 0;
    public bool isGrounded = false;
    public bool jumpPower = false;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("floor")) { numWallsTouching++; isGrounded = true; }
        if (c.gameObject.CompareTag("jumpPower")) { jumpPower = true; }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("floor")) { 
            numWallsTouching--;
            isGrounded = numWallsTouching > 0;
        }
    }
}
