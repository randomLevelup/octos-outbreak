using UnityEngine;

public class rbIsGrounded : MonoBehaviour
{
    public bool isGrounded = false;
    public bool jumpPower = false;

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("floor")) { isGrounded = true; }
        if (c.gameObject.CompareTag("jumpPower")) { jumpPower = true; }
    }

    private void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("floor")) { isGrounded = false; }
    }
}
