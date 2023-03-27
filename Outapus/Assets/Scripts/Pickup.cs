using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool isFollowingPlayer = false;
    private Transform player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFollowingPlayer = true;
            player = other.gameObject.transform;
        }
    }

    // void Update()
    // {
    //     Debug.Log(jumpPower);
    //
    //     // transform.LookAt(Vector2.left);
    //     float x = Input.GetAxis("Horizontal");
    //
    //     Vector2 movement = new Vector2(x,0);
    //     transform.Translate(movement * speed * Time.deltaTime);
    //
    //     if (Input.GetKeyDown(KeyCode.Space)) {
    //         if(onGround) {
    //             float tempJump = 10;
    //             if (jumpPower) {
    //                 tempJump = 15;
    //                 jumpPower = false;
    //             }
    //             rb.AddForce(Vector2.up * tempJump, ForceMode2D.Impulse);
    //         }
    //     }
    // }

    private void FixedUpdate()
    {
        if (!isFollowingPlayer)
        {
            // Apply gravity to the object
            rb.AddForce(Vector2.down * Physics2D.gravity.magnitude * rb.mass);
        }
        else
        {
            // Make the object follow the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.fixedDeltaTime * 5f);
        }
    }
}
