using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// should combine this with jump script after putting on octopus, so can program to only make noise when jumping. 
public class mic_jumpSFX : MonoBehaviour
{
    AudioSource jumpSFX;
    // Start is called before the first frame update
    void Start()
    {
        jumpSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpSFX.Play();
        }
    }
}
