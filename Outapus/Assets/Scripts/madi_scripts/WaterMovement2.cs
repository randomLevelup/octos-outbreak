using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private ConstantForce2D force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(0, -0.01f, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("test");
            //transform.Translate(Vector3.down * Time.deltaTime / 0.2f);
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector3.up * 0.2f, ForceMode2D.Impulse);
        }
    }

    
}
