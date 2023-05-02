using UnityEngine;

public class EyeMovementB : EyeMovement
{
    private float lookDistance = 0.06f;
    private float easeAmount = 1f;

    private void FixedUpdate()
    {
        Vector2 displace = rb.transform.position - cam.transform.position;
        float mag = displace.magnitude;
        float newMag = lookDistance * (1f + (-easeAmount / (mag + easeAmount)));
        Vector2 res = displace.normalized * newMag;
        transform.localPosition = res;
    }
}