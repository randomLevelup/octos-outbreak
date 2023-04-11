using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JupiterSwing : MonoBehaviour
{
    public GameObject hingeFolder;
    public Transform abdomenObject;
    public LineRenderer lr;
    public float baseBubbleSize = 3.74f;
    public bool swinging = false;

    private SpringJoint2D currentJoint;
    private Rigidbody2D abdomenRB;

    private void Start()
    {
        abdomenRB = abdomenObject.GetComponent<Rigidbody2D>();
        lr.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!swinging)
            {
                SpringJoint2D[] allJoints = hingeFolder.GetComponentsInChildren<SpringJoint2D>();
                foreach (SpringJoint2D joint in allJoints)
                {
                    float swingRadius = CalculateRadius(joint.gameObject);

                    if (Vector3.Distance(joint.transform.position, abdomenObject.position) < swingRadius)
                    {
                        currentJoint = joint;
                        currentJoint.connectedBody = abdomenRB;
                        swinging = true;
                        lr.enabled = true;
                    }
                }

            } else
            {
                currentJoint.connectedBody = null;
                swinging = false;
                lr.enabled = false;
            }
        }
    }

    private float CalculateRadius(GameObject hingeObject)
    {
        Transform bubbleTransform = hingeObject.GetComponentsInChildren<Transform>()[1];
        return bubbleTransform.localScale.x * baseBubbleSize;
    }
}
