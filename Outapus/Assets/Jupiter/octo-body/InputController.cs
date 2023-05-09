using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Rigidbody2D AbdomenObject;
    public AudioSource walk1;
    public AudioSource walk2;
    public AudioSource pause;
    private int WalkClip = 1;
    private rbIsGrounded rbControlState;

    public float groundPush = 100f;
    public float airPush = 50f;

    private void Start()
    {
        rbControlState = AbdomenObject.GetComponent<rbIsGrounded>();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            float pushStrength = rbControlState.isGrounded ? groundPush : airPush;
            Vector2 xForce = new Vector2(Input.GetAxisRaw("Horizontal") * pushStrength, 0);
            AbdomenObject.AddForce(xForce);

            if(rbControlState.isGrounded) {
                if(!walk1.isPlaying && !walk2.isPlaying && !pause.isPlaying) {
                    if (WalkClip == 1){
                        walk2.volume = JupiterGameHandler.volumeLevel;
                        walk2.Play();
                        pause.Play();
                        WalkClip = 2;
                    }
                    else {
                        walk1.volume = JupiterGameHandler.volumeLevel;
                        Debug.Log("pause");
                        walk1.Play();
                        pause.Play();
                        WalkClip = 1;
                    }
                }
            }
        }


    }
}
