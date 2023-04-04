using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingHandler : MonoBehaviour
{
    public Rigidbody2D octopus;
    private SpringJoint2D joint;
    public Rigidbody2D connectionBody;
    public Transform crosshair;
    bool swinging = true;
    public int jumpHeight;
    //private bool grounded = true;
    public LineRenderer lr;
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
        //create new connection
        if(Input.GetMouseButtonDown(0)){
            
            connectionBody.position = crosshairPos;
            joint.connectedBody = connectionBody;
            swinging = true;
            lr.enabled = true;
        }
    
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(swinging){
                
                joint.connectedBody = octopus;
                swinging = false;
                octopus.AddForce(new Vector3(0, jumpHeight, 0));
                lr.enabled = false;
            }
            /*
            else if (grounded){
                octopus.AddForce(new Vector3(0, jumpHeight, 0));
                grounded = false;
            }
            */
        }
    }
    /*
    //on collsiion enter tag with floot grouned = true;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "floor"){
            grounded = true;
        }
    }
    */

}
