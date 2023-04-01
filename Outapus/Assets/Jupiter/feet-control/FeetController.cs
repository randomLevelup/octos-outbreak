using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FeetController : MonoBehaviour
{
    public Transform abdomenObject;
    public Transform rayHeadObject;

    private Vector2 gazePos;
    public float gazeDistance = 2f;

    public int numberOfRays = 10;
    public float rayReachDistance = 4f;
    public float rayAngleRange = 160f;
    public int rayColliderLayerIndex = 6;
    private List<Vector2> hits;

    private Vector2[] targetPoints;
    private Vector2[] feetPosCurrent;
    private Vector2[] feetPosFrom;
    private Vector2?[] feetPosTo;

    private float[] tValues;
    private AnimationCurve curveEase;
    public float footVelocity = 0.05f;

    public float retargetThreshold = 2.5f;
    public float feetTargetRadius = 0.1f;

    public int numberOfFeet = 4;
    public float tentacleHeightOffset = 3.6f;
    public GameObject[] tentacleObjects;

    private float gizmoAngle;
    private bool drawGizmoAngle;

    private void Start()
    {
        drawGizmoAngle = false;

        numberOfFeet = tentacleObjects.Length;
        targetPoints = new Vector2[numberOfFeet];

        feetPosCurrent = new Vector2[numberOfFeet];
        feetPosFrom = new Vector2[numberOfFeet];
        feetPosTo = new Vector2?[numberOfFeet];

        tValues = new float[numberOfFeet];
        curveEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

        hits = new List<Vector2>();

        for (int i=0; i<numberOfFeet; i++)
        {
            targetPoints[i] = new Vector2(0, 100);
            feetPosCurrent[i] = Vector2.zero;
            feetPosFrom[i] = Vector2.zero;
            tValues[i] = 0f;
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

        // update feet position using spherical interpolation
        RetargetFeet();

        // update all tentacle game objects
        RepositionGameObjects();
    }

    private void RepositionGameObjects()
    {
        for (int i=0; i<numberOfFeet; i++)
        {
            Vector3 ikBasePos = abdomenObject.position;
            ikBasePos.y += tentacleHeightOffset;
            tentacleObjects[i].transform.position = ikBasePos;

            TentacleTargetInterface targetComponent = tentacleObjects[i].GetComponent<TentacleTargetInterface>();
            targetComponent.targetTransform.position = feetPosCurrent[i];
        }
    }

    private void RetargetFeet()
    {
        for (int i=0; i<numberOfFeet; i++) {
            if (feetPosTo[i] != null)
            {
                if (Vector2.Distance(feetPosCurrent[i], (Vector2)feetPosTo[i]) > feetTargetRadius) {
                    float smoothT = curveEase.Evaluate(tValues[i]);
                    //feetPosCurrent[i] = Vector2.Lerp(feetPosFrom[i], (Vector2)feetPosTo[i], smoothT);
                    feetPosCurrent[i] = Vector3.Slerp(feetPosFrom[i], (Vector3)feetPosTo[i], smoothT);
                    tValues[i] += footVelocity;

                } else
                {
                    feetPosFrom[i] = (Vector2)feetPosTo[i];
                    feetPosTo[i] = null;
                }
            }
        }
    }

    private void RetargetTargets()
    {
        for (int i=0; i<targetPoints.Length; i++)
        {
            // retarget points if they are further away than allowed threshold (#1)
            if (hits.Count > 0 && Vector2.Distance(gazePos, targetPoints[i]) > retargetThreshold)
            {
                int chosenHitIndex = Random.Range(0, hits.Count - 1);

                feetPosFrom[i] = targetPoints[i];
                targetPoints[i] = hits[chosenHitIndex];
                feetPosTo[i] = targetPoints[i];
                tValues[i] = 0;
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

        //Gizmos.color = Color.yellow;
        //if (hits != null)
        //{
        //    foreach (Vector2 hit in hits)
        //    {
        //        Gizmos.DrawLine(rayHeadObject.position, hit);
        //    }
        //}

        //if (drawGizmoAngle)
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawLine(rayHeadObject.position, (Vector2)rayHeadObject.position + new Vector2
        //    (
        //        Mathf.Cos(gizmoAngle) * rayReachDistance,
        //        Mathf.Sin(gizmoAngle) * rayReachDistance
        //    ));
        //}

        Gizmos.color = Color.gray;
        if (targetPoints != null)
        {
            foreach (Vector2 targetPoint in targetPoints)
            {
                Gizmos.DrawSphere(targetPoint, 0.18f);
            }
        }

        Gizmos.color = Color.cyan;
        if (feetPosCurrent != null)
        {
            foreach (Vector2? foot in feetPosCurrent)
            {
                if (foot != null)
                {
                    //Gizmos.DrawSphere((Vector3)foot, 0.18f);
                    Gizmos.DrawLine(abdomenObject.position, (Vector3)foot);
                }
            }
        }
    }
}
