using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform target; // the player
    public float smoothing = 0.6f; // how quickly camera moves to player
    public Vector2 minPosition; // X and Y values for lower left corner of play area
    public Vector2 maxPosition; // X and Y values for upper right corner

    void Update()
    {
        if (transform.position != target.position)
        {
            Vector3 targPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            targPos.x = Mathf.Clamp(targPos.x, minPosition.x, maxPosition.x);
            targPos.y = Mathf.Clamp(targPos.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targPos, smoothing);
        }
    }
}