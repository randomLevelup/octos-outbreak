using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mic_double_jump : MonoBehaviour
{

    [SerializeField] float jump = 10;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Vector2 fastFallTranslate;
    private bool onGround = false;
    private bool doubleJump = false;
    private bool slowFall = false;
    private bool fastFall = false;
    private float fastFallSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        fastFallTranslate = new Vector2(0,-fastFallSpeed);

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(Vector2.left);

        if (slowFall) {
            rb.AddForce(Vector2.up * 1);
            if(onGround) {
                slowFall = false;
            }
        }

        
        if (fastFall) {
            // rb.AddForce(Vector2.down * fastFallSpeed);
            transform.Translate(fastFallTranslate * Time.deltaTime);
            if(onGround) {
                fastFall = false;
            }
        }

        // dynamic sprite color
        if(doubleJump) {
            sprite.color = new Color (1, (float)0.50, (float)0.016, 1);
        }
        else if (slowFall) {
            sprite.color = new Color (1, (float)0.70, 0, 1);
        }
        else {
            sprite.color = new Color (100,100,100);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if(onGround) {
                rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            }
            else {
                if(doubleJump) {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                    doubleJump = false;
                    slowFall = true;
                }
                else if(slowFall) {
                    rb.AddForce(Vector2.down * fastFallSpeed);
                    rb.velocity = Vector2.zero;
                    fastFall = true;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            onGround = true;
        }
        if (collision.gameObject.CompareTag("doubleJumpPower")) {
            doubleJump = true;
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            onGround = false;
        }
    }
}
