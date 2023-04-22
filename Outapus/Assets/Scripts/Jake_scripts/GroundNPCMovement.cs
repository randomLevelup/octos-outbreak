using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GroundNPCMovement : MonoBehaviour
{
  // https://www.youtube.com/watch?v=2SXa10ILJms
    public GameObject player;
    public float speed;
    public float jumpHeight;
    private float distance;
    private Rigidbody2D rb;
    private singleJumpIsGrounded groundState;
    private isShadowed shadowState;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        groundState = this.GetComponent<singleJumpIsGrounded>();
        shadowState = player.GetComponent<isShadowed>();
    }

    // Update is called once per frame
    void Update()
    {
        //don't accelerate if in the air or palyer is under shadow
        if(shadowState.shadowed == false ){
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
            if(angle > 0){
                Jump();
            }
            rb.AddForce(new Vector3(direction.x * Time.deltaTime * speed, 0, 0));
        }
        //when in air, reduce linear drag
        else if(!groundState.isGrounded){
            rb.drag = 0;
        }
        else {
            rb.drag = 1;
        }
    }
    
    void Jump(){
        if(groundState.isGrounded){
            rb.AddForce(new Vector3(0, jumpHeight, 0));
        }
    }
}
