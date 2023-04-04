using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Rigidbody2D AbdomenObject;
    private rbIsGrounded rbControlState;

    public float groundPush = 100f;
    public float airPush = 50f;

    private void Start()
    {
        rbControlState = AbdomenObject.GetComponent<rbIsGrounded>();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            float pushStrength = rbControlState.isGrounded ? groundPush : airPush;
            Vector2 xForce = new Vector2(Input.GetAxisRaw("Horizontal") * pushStrength, 0);
            AbdomenObject.AddForce(xForce);
        }
    }
}
