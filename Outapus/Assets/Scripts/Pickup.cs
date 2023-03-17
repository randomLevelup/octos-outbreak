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
