using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSwingHandler : MonoBehaviour
{
    public GameObject hingeFolder;
    public Transform abdomenObject;
    public LineRenderer lr;
    public float baseBubbleSize = 3.74f;
    public bool swinging = false;
    private SpringJoint2D[] allJoints;
    public GameObject SwingController;
    private LineController lineControllerScript;
    private rbIsGrounded groundState;
    public int boost;


    private SpringJoint2D currentJoint;
    private Rigidbody2D abdomenRB;

    private void Start()
    {
        allJoints = hingeFolder.GetComponentsInChildren<SpringJoint2D>();
        abdomenRB = abdomenObject.GetComponent<Rigidbody2D>();
        lr.enabled = false;
        lineControllerScript = SwingController.GetComponent<LineController>();
        groundState = abdomenObject.GetComponent<rbIsGrounded>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundState.isGrounded == false)
        {
            if (!swinging)
            {
                int closestIndex = nearestJointIndex();
                currentJoint = allJoints[closestIndex];
                float swingRadius = CalculateRadius(currentJoint.gameObject);

                if (Vector3.Distance(currentJoint.transform.position, abdomenObject.position) < swingRadius)
                {
                    lineControllerScript.hinge = currentJoint.transform;
                    currentJoint.connectedBody = abdomenRB;
                    swinging = true;
                    lr.enabled = true;
                }

            } 
            else
            {
                currentJoint.connectedBody = null;
                swinging = false;
                lr.enabled = false;
                //add force
                abdomenRB.AddForce(abdomenRB.velocity * boost / abdomenRB.velocity.magnitude);
            }
        }
    }

    private float CalculateRadius(GameObject hingeObject)
    {
        Transform bubbleTransform = hingeObject.GetComponentsInChildren<Transform>()[1];
        return bubbleTransform.localScale.x * baseBubbleSize;
    }
    
    private int nearestJointIndex(){
        int nearestIndex = 0;
        float smallestDistance = float.MaxValue;
        for(int i = 0; i < allJoints.Length; i++){
            float distance = Vector3.Distance (allJoints[i].transform.position, abdomenObject.position);
            if(distance < smallestDistance){
                smallestDistance = distance;
                nearestIndex = i;
            } 
        }
        return nearestIndex;
    }
}
