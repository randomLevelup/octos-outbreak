using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform abdomen;
    public Transform bounds;
    public float trackCoefficient = 3.5f;

    private AnimationCurve trackEase;
    private AnimationCurve boundsEase;

    private Vector2 wallBoundsMin;
    private Vector2 wallBoundsMax;

    private float deltaX;
    private float deltaY;

    private void Start()
    {
        float v = trackCoefficient / 10;
        trackEase = AnimationCurve.EaseInOut(-10, -v, 10, v);
        boundsEase = AnimationCurve.EaseInOut(10, 1, 5, 0);

        wallBoundsMin = bounds.position - (bounds.localScale / 2);
        wallBoundsMax = bounds.position + (bounds.localScale / 2);
    }

    private void Update()
    {
        Vector2 vec = abdomen.position - transform.position;
        deltaX = trackEase.Evaluate(vec.x);
        deltaY = trackEase.Evaluate(vec.y);

        deltaX *= boundsEase.Evaluate(transform.position.x - wallBoundsMin.x);
        deltaX *= boundsEase.Evaluate(wallBoundsMax.x - transform.position.x);

        deltaY *= boundsEase.Evaluate(transform.position.y - wallBoundsMin.y);
        deltaY *= boundsEase.Evaluate(wallBoundsMax.y - transform.position.y);
    }

    private void FixedUpdate() { transform.position += new Vector3(deltaX, deltaY); }
}
