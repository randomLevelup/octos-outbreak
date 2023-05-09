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
      private GameObject bubble;
      private Vector3 pos;

      private void Start()
      {
        Debug.Log("test");
            rb = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource> ();
            bubble = transform.GetChild(0).gameObject;
            pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
      }

      private void FixedUpdate()
      {
          if (isFollowingPlayer)
          {
            bubble.SetActive(false);
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
                          _audioSource.volume = JupiterGameHandler.volumeLevel;
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
                  bubble.SetActive(true);
            }
          }
      }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes")) {
            transform.position = pos;
        }
    }

}
