using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
private AudioSource _audioSource;

public float pickupRange = 1.5f; // Adjust this to change the range at which the player can pick up objects
private bool isFollowingPlayer = false;
private Transform player;
private Rigidbody2D rb;

private void Start()
{
    rb = GetComponent<Rigidbody2D>();
    _audioSource = GetComponent<AudioSource> ();
}

private void FixedUpdate()
{
    if (isFollowingPlayer)
    {
        // Make the object follow the player
        transform.position = player.position;
    }
}

private void Update()
{
    if (Input.GetKeyDown(KeyCode.P))
    {
        if (!isFollowingPlayer)
        {
            // Check if the player is touching the object
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRange);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    isFollowingPlayer = true;
                    player = collider.gameObject.transform;

                    _audioSource.pitch = 1f;
                    _audioSource.Play();
                    break;
                }
            }
        }
        else
        {
            // Release the object
            isFollowingPlayer = false;
            player = null;
        }
    }
}

  // private bool isStuck = false;
  // private GameObject stickyObject;
  // private Vector2 stuckOffset;
  //
  // private bool isInRangeOfPlayer = false;
  //
  // private void Update()
  // {
  //     if (Input.GetKeyDown(KeyCode.P) && isInteracting)
  //     {
  //
  //     }
  // }
  //
  // // private void followPlayer()
  // // {
  // //
  // // }
  //
  // private void OnCollisionEnter2D(Collision2D collision)
  // {
  //     if (collision.gameObject.CompareTag("Player"))
  //     {
  //         isStuck = true;
  //         stickyObject = collision.gameObject;
  //         stuckOffset = transform.position - (Vector3)stickyObject.transform.position;
  //
  //         if (stuckOffset.y < 0f)
  //         {
  //             stuckOffset.y = 0f;
  //         }
  //
  //         SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
  //         if (Mathf.Abs(stuckOffset.x) < spriteRenderer.bounds.size.x)
  //         {
  //             stuckOffset.x = spriteRenderer.bounds.size.x;
  //         }
  //     }
  // }
  //
  // private void FixedUpdate()
  // {
  //     if (isStuck && stickyObject != null)
  //     {
  //         Vector3 targetPosition = stickyObject.transform.position + (Vector3)stuckOffset;
  //         transform.position = targetPosition;
  //     }
  // }

}
