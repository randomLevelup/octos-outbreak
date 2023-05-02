using UnityEngine;

public abstract class EyeMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
}

public class EyeMovementA : EyeMovement
{
    private float lookDistance = 0.06f;
    private float easeAmount = 0.45f;

    private void FixedUpdate()
    {
        Vector2 vel = rb.velocity;
        float mag = vel.magnitude;

        // calculate new magnitude with a custom function
        float newMag = lookDistance * (1f + (-easeAmount / (mag + easeAmount)));
        Vector2 res = vel.normalized * newMag;

        // update pupil position based on new vector
        transform.localPosition = res;
    }
}
