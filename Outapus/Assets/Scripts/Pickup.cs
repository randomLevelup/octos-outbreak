using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

  private bool isStuck = false; // Track whether this object is stuck to another object
  private GameObject stickyObject; // Reference to the object that this object is stuck to (if any)
  private Vector2 stuckOffset; // Offset between this object and the sticky object when they collided

  private void OnCollisionEnter2D(Collision2D collision)
  {
      if (collision.gameObject.CompareTag("Player"))
      {
          isStuck = true;
          stickyObject = collision.gameObject;
          stuckOffset = transform.position - (Vector3)stickyObject.transform.position;

          // If the stuck offset is below the player, adjust it to be at least at the player's level
          if (stuckOffset.y < 0f)
          {
              stuckOffset.y = 0f;
          }

          SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
          if (stuckOffset.x < spriteRenderer.bounds.size.x)
          {
              stuckOffset.x = spriteRenderer.bounds.size.x;
          }
      }
  }

  private void FixedUpdate()
  {
      if (isStuck && stickyObject != null)
      {
          // Calculate the target position based on the sticky object's position and the stuck offset
          Vector3 targetPosition = stickyObject.transform.position + (Vector3)stuckOffset;
          transform.position = targetPosition;
      }
  }




    // private bool isFollowingPlayer = false;
    // private Transform player;
    // private Rigidbody2D rb;
    // private bool isStuck = false;
    // private GameObject stickyObject;
    // private Vector2 stuckOffset;
    //
    // private void Start()
    // {
    //     // rb = GetComponent<Rigidbody2D>();
    // }
    //
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         // isFollowingPlayer = true;
    //         // player = other.gameObject.transform;
    //
    //         isStuck = true;
    //         stickyObject = other.gameObject;
    //         stuckOffset = transform.position - stickyObject.transform.position;
    //         //transform.parent = stickyObject.transform;
    //     }
    // }
    //
    // void Update()
    // {
    //     if (isStuck && stickyObject != null)
    //     {
    //         // Move this object along with the sticky object
    //         Vector2 newPosition = ()stickyObject.transform.position + stuckOffset;
    //         transform.position = stickyObject.transform.position;
    //     }
    // }


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

    // private void FixedUpdate()
    // {
    //     if (!isFollowingPlayer)
    //     {
    //         // Apply gravity to the object
    //         rb.AddForce(Vector2.down * Physics2D.gravity.magnitude * rb.mass);
    //     }
    //     else
    //     {
    //         // Make the object follow the player
    //         transform.position = Vector2.MoveTowards(transform.position, player.position, Time.fixedDeltaTime * 5f);
    //     }
    // }
}
