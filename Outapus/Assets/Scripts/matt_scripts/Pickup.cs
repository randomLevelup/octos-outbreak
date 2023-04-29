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
              //transform.position = player.position;
              transform.position = new Vector3(player.position.x - 1, player.position.y, player.position.z);
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

}
