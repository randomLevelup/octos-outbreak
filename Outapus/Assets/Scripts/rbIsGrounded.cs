using UnityEngine;

public class rbIsGrounded : MonoBehaviour
{
    public int numWallsTouching = 0;
    public bool isGrounded = false;
    public bool jumpPower = false;

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("floor")) { numWallsTouching++; isGrounded = true; }
        if (c.gameObject.CompareTag("jumpPower")) { jumpPower = true; }
    }

    private void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("floor")) { 
            numWallsTouching--;
            isGrounded = numWallsTouching > 0;
        }
    }
}
