using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Rigidbody2D player;
    private rbIsGrounded controlState;
    private bool inWater;

    // Start is called before the first frame update
    void Start()
    {
        inWater = false;
        controlState = player.GetComponent<rbIsGrounded>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            //transform.Translate(Vector3.down * Time.deltaTime / 0.2f);
            //player.velocity = Vector2.zero;
            if (inWater) {
                //Debug.Log("test");
                player.AddForce(Vector3.up * 0.08f, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("water");
        if (other.gameObject.CompareTag("head"))
        {
            inWater = true;
            Debug.Log("water");
            //controlState.isGrounded = true;
            player.gravityScale = 0.3f;
            player.velocity = new Vector3(0f, -3f, 0f);
            //player.GetComponent<ConstantForce2D>().force = new Vector3(0, -0.2f, 0);
            

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("water");
        if (other.gameObject.CompareTag("head"))
        {
            inWater = true;
            //Debug.Log("water");
            //player.velocity = new Vector3(0f, -2f, 0f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("leave water");
        inWater = false;
        //player.velocity = new Vector3(0f, 0f, 0f);
        //player.GetComponent<ConstantForce2D>().force = new Vector3(0, 0, 0);
        player.gravityScale = 2.25f;
        //player.AddForce(Vector3.up * 0.15f, ForceMode2D.Impulse);
        player.AddForce(Vector2.up * 0.15f, ForceMode2D.Impulse);
    }

}
