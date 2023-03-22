using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour
{
    public Transform abdomenObject;
    public Transform rayHeadObject;

    public Vector2 gazePos;
    public float gazeDistance = 2f;

    public int numberOfRays = 10;
    public float rayReachDistance = 4f;
    public float rayAngleRange = 160f;
    public int rayColliderLayerIndex = 6;
    private List<Vector2> hits;

    public int numberOfFeet = 4;
    public Vector2[] targetPoints;
    public Vector2[] feetPositions;

    public float retarget1Threshold = 2.5f;
    public float retarget2Threshold = 2.5f;

    private float gizmoAngle;
    private bool drawGizmoAngle;

    private void Start()
    {
        drawGizmoAngle = false;

        targetPoints = new Vector2[numberOfFeet];
        feetPositions = new Vector2[numberOfFeet];
        hits = new List<Vector2>();

        for (int i=0; i<numberOfFeet; i++)
        {
            targetPoints[i] = new Vector2(0, 100);
            feetPositions[i] = Vector2.zero;
        }
    }

    private void Update()
    {
        // update "gaze" position towards the input controller axis
        gazePos.x = abdomenObject.position.x + (Input.GetAxisRaw("Horizontal") * gazeDistance);
        gazePos.y = abdomenObject.position.y + (
            Mathf.Max(0, Input.GetAxisRaw("Vertical") * gazeDistance)
        );

        // raycast from head position
        hits.Clear();
        CastRays();

        // update target points if they are far from body
        RetargetTargets();
    }

    private void RetargetTargets()
    {
        for (int i=0; i<targetPoints.Length; i++)
        {
            // retarget points if they are further away than allowed threshold (#1)
            if (hits.Count > 0 && Vector2.Distance(gazePos, targetPoints[i]) > retarget1Threshold)
            {
                int chosenHitIndex = Random.Range(0, hits.Count - 1);
                targetPoints[i] = hits[chosenHitIndex];
            }
        }
    }

    private void CastRays()
    {
        float inputAxisAngle = getInputAxisAngle();
        DrawAngleGizmo(inputAxisAngle);

        float thetaMin = inputAxisAngle - (rayAngleRange / 2);
        float thetaMax = inputAxisAngle + (rayAngleRange / 2);
        float thetaStep = rayAngleRange / numberOfRays;

        Vector2 rayDirection;

        for (float t=thetaMin; t<=thetaMax; t += thetaStep)
        {
            rayDirection = new Vector2( Mathf.Cos(t), Mathf.Sin(t) );

            RaycastHit2D hit = Physics2D.Raycast
            (
                rayHeadObject.position,
                rayDirection,
                rayReachDistance
            );

            if (hit.collider != null)
            {
                hits.Add(hit.point);
            }
        }
    }

    private float getInputAxisAngle()
    {
        Vector2 gazeDirection = gazePos - (Vector2)rayHeadObject.position;
        float res = Mathf.Atan2(gazeDirection.y, gazeDirection.x);
        return res;
    }

    private void DrawAngleGizmo(float angle)
    {
        gizmoAngle = angle;
        drawGizmoAngle = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gazePos, 0.18f);

        Gizmos.color = Color.yellow;
        if (hits != null)
        {
            foreach (Vector2 hit in hits)
            {
                Gizmos.DrawLine(rayHeadObject.position, hit);
            }
        }

        if (drawGizmoAngle)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(rayHeadObject.position, (Vector2)rayHeadObject.position + new Vector2
            (
                Mathf.Cos(gizmoAngle) * rayReachDistance,
                Mathf.Sin(gizmoAngle) * rayReachDistance
            ));
        }

        Gizmos.color = Color.gray;
        foreach (Vector2 targetPoint in targetPoints)
        {
            Gizmos.DrawSphere(targetPoint, 0.18f);
        }
    }
}
