using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Rigidbody2D AbdomenObject;
    public float pushStrength = 100f;

    private void FixedUpdate()
    {
        Vector2 xForce = new Vector2(Input.GetAxisRaw("Horizontal") * pushStrength, 0);
        AbdomenObject.AddForce(xForce);
    }
}
