using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("test");
            //transform.Translate(Vector3.down * Time.deltaTime / 0.2f);
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector3.down * 2, ForceMode2D.Impulse);
        }
    }
}
