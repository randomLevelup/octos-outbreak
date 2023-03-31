using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

  private bool isStuck = false;
  private GameObject stickyObject;
  private Vector2 stuckOffset;

  private void OnCollisionEnter2D(Collision2D collision)
  {
      if (collision.gameObject.CompareTag("Player"))
      {
          isStuck = true;
          stickyObject = collision.gameObject;
          stuckOffset = transform.position - (Vector3)stickyObject.transform.position;

          if (stuckOffset.y < 0f)
          {
              stuckOffset.y = 0f;
          }

          SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
          if (Mathf.Abs(stuckOffset.x) < spriteRenderer.bounds.size.x)
          {
              stuckOffset.x = spriteRenderer.bounds.size.x;
          }
      }
  }

  private void FixedUpdate()
  {
      if (isStuck && stickyObject != null)
      {
          Vector3 targetPosition = stickyObject.transform.position + (Vector3)stuckOffset;
          transform.position = targetPosition;
      }
  }

}
