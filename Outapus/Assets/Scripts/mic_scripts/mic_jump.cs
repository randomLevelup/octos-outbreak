using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mic_jump : MonoBehaviour
{
    [SerializeField] float jump = 10;
    [SerializeField] float speed = 5;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    private bool onGround = false;
    private bool jumpPower = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(jumpPower);

        // transform.LookAt(Vector2.left);
        float x = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(x,0);
        transform.Translate(movement * speed * Time.deltaTime);

        if(jumpPower) {
            sprite.color = new Color (50,100,0);
        }
        else {
            sprite.color = new Color (100,100,100);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if(onGround) {
                float tempJump = 10;
                if (jumpPower) {
                    tempJump = 15;
                    jumpPower = false;
                }
                rb.AddForce(Vector2.up * tempJump, ForceMode2D.Impulse);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            onGround = true;
        }
        if (collision.gameObject.CompareTag("jumpPower")) {
            jumpPower = true;
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            onGround = false;
        }
    }
    // void OnCollisionEnter (Collider other) {
    //     if(other.tag == "floor") {
    //         onGround = true;
    //     }
    // }

    // void OnCollisionExit (Collider other) {
    //     if(other.tag == "floor") {
    //         onGround = false;
    //     }
    // }
}
