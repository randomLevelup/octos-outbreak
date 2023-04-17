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
    public float rayAngleRange = 1.7f;
    public float rayFloorBias = 0.443f;
    public int rayColliderLayerIndex = 6;
    private int raycastLayerMask;
    private List<Vector2> hits;

    private Vector2[] targetPoints;
    private Vector2[] feetPosCurrent;
    private Vector2[] feetPosFrom;
    private Vector2?[] feetPosTo;

    private float[] tValues;
    private AnimationCurve curveEase;
    public float footVelocity = 0.05f;
    public float flailAmount = 2.4f;

    public float retargetThreshold = 2.5f;
    public float minTargetRadius = 0.1f;
    public float maxTargetRadius = 2f;

    public float targetImportance = 4.74f;
    public float targetSwitchIntensity = 4.3f;

    public int numberOfFeet = 4;
    public float tentacleHeightOffset = 3.6f;
    public GameObject[] tentacleObjects;
    private float[] tentacleRotations;

    private float gizmoAngle;
    private bool drawGizmoAngle;

    private void Start()
    {
        drawGizmoAngle = false;

        numberOfFeet = tentacleObjects.Length;
        targetPoints = new Vector2[numberOfFeet];
        tentacleRotations = new float[numberOfFeet];

        feetPosCurrent = new Vector2[numberOfFeet];
        feetPosFrom = new Vector2[numberOfFeet];
        feetPosTo = new Vector2?[numberOfFeet];

        tValues = new float[numberOfFeet];
        curveEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

        raycastLayerMask = 1 << rayColliderLayerIndex;
        hits = new List<Vector2>();

        for (int i=0; i<numberOfFeet; i++)
        {
            targetPoints[i] = new Vector2(0, 100);
            feetPosCurrent[i] = Vector2.zero;
            feetPosFrom[i] = Vector2.zero;
            tValues[i] = 0f;

            float rand = Random.Range(-50f, 50f);
            Quaternion randRot = Quaternion.Euler(0f, 0f, rand);
            tentacleObjects[i].transform.rotation = randRot;
            tentacleRotations[i] = rand;
        }
    }

    private void Update()
    {
        //update "gaze" position towards the input controller axis
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

    private void FixedUpdate() { RepositionGameObjects(); }

    // cast rays to find target points; save all hits to an arraylist
    private void CastRays()
    {
        float inputAxisAngle = GetVelocityVector();
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
                rayReachDistance,
                raycastLayerMask
            );

            if (hit.collider != null)
            {
                hits.Add(hit.point);
            }
        }
    }

    // retarget points if they are further away than allowed threshold
    private void RetargetTargets()
    {
        for (int i=0; i<targetPoints.Length; i++)
        {
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

    // translate foot position along an eased curve for smooth "stepping"
    private void RetargetFeet()
    {
        float smoothT;

        for (int i=0; i<numberOfFeet; i++) {
            if (feetPosTo[i] != null)
            {
                float footDistance = Vector2.Distance(feetPosCurrent[i], (Vector2)feetPosTo[i]);
                if (footDistance > minTargetRadius) {
                    if (footDistance > maxTargetRadius)
                    {
                        // instantly snap targets if they are too far
                        tValues[i] = 1f;
                        smoothT = curveEase.Evaluate(1f);

                    } else { smoothT = IncrementTValue(i); }

                    feetPosCurrent[i] = Vector3.Slerp(feetPosFrom[i], (Vector3)feetPosTo[i], smoothT);

                } else
                {
                    feetPosFrom[i] = (Vector2)feetPosTo[i];
                    feetPosTo[i] = null;
                }
            }
        }
    }

    // update actual ik targets for each tentacle
    private void RepositionGameObjects()
    {
        for (int i=0; i<numberOfFeet; i++)
        {
            // get distance from abdomen to foot target, then compute function
            // to clamp/interpolate between current pos and hover pos
            float dist = Vector2.Distance(feetPosCurrent[i], abdomenObject.position);
            Vector2 newPos = ImportanceSlerp(feetPosCurrent[i], CalculateHoverPos(i), dist);

            // adjust ikBase transform
            // adjust tentacleTarget transform to function result
            tentacleObjects[i].transform.position = AdjustIkBase(i);
            TentacleTargetInterface targetComponent = tentacleObjects[i].GetComponent<TentacleTargetInterface>();
            targetComponent.targetTransform.position = newPos;
        }

        RetargetFeet();
    }

    // returns the direction of velocity; shifts extreme angles towards the floor
    private float GetVelocityVector()
    {
        Vector2 vel = abdomenObject.GetComponent<Rigidbody2D>().velocity;
        if (vel.magnitude < 0.01) { return -(Mathf.PI/2); }

        float res = Mathf.Atan2(vel.y, vel.x);
        float floorBias = Mathf.Max(-rayFloorBias * Mathf.Cos(res), 0);
        res += floorBias * ((res < 0) ? -1 : 1);
        return res;
    }

    // evaluate the smooth step curve
    private float IncrementTValue(int index)
    {
        float res = curveEase.Evaluate(tValues[index]);
        tValues[index] += footVelocity;
        return res;
    }

    // calculate targeting importance using a sigmoid interpolation
    private Vector2 ImportanceSlerp(Vector2 a, Vector2 b, float d)
    {
        float res = -(d - targetImportance);
        res = Mathf.Pow(targetSwitchIntensity, res);
        res = 1 / (1 + res);
        return Vector2.Lerp(a, b, res);
    }

    private Vector2 CalculateHoverPos(int index)
    {
        Vector2 res = (Vector2)abdomenObject.position - new Vector2(0f, 0.5f);
        res = OffsetWithNoise(res, index, Time.fixedTime);
        return res;
    }

    private Vector2 OffsetWithNoise(Vector2 pos, int p1, float p2)
    {
        pos.x += (Mathf.PerlinNoise(p1, p2 + p1) - 0.5f) * flailAmount;
        p1 += numberOfFeet;
        pos.y += (Mathf.PerlinNoise(p1, p2 + p1) - 0.5f) * flailAmount;
        return pos;
    }

    public Vector3 AdjustIkBase(int i)
    {
        Vector3 res = rayHeadObject.transform.position + new Vector3(0, -0.5f);
        float theta = tentacleRotations[i] * Mathf.Deg2Rad;

        res.x += tentacleHeightOffset * Mathf.Sin(theta);
        res.y += tentacleHeightOffset + (-Mathf.Cos(theta) - 1);

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
                    Gizmos.DrawSphere((Vector3)foot, 0.18f);
                }
            }
        }
    }
}
