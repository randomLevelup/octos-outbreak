using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// should combine this with jump script after putting on octopus, so can program to only make noise when jumping.
public class SwingSFX : MonoBehaviour
{
    AudioSource swingSFX;
    // Start is called before the first frame update
    void Start()
    {
        swingSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            swingSFX.volume = JupiterGameHandler.volumeLevel;
            swingSFX.Play();
        }
    }
}
