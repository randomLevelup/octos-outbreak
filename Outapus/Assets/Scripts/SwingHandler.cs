using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingHandler : MonoBehaviour
{
    public GameObject octopus;
    private SpringJoint2D joint;
    public Rigidbody2D connectionBody;
    public Transform crosshair;
    bool swinging = true;
    // Start is called before the first frame update
    void Start()
    {
        joint = octopus.GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //update cross hair
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 crosshairPos = new Vector3(mousePos.x, mousePos.y,0);
        
        crosshair.position = crosshairPos;
        if(Input.GetMouseButtonDown(0)){
            connectionBody.position = crosshairPos;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(swinging){
                joint.connectedBody = octopus.GetComponent<Rigidbody2D>();
            }
            else {
                joint.connectedBody = connectionBody;
            }
            swinging = !swinging;
        }
    }
}
