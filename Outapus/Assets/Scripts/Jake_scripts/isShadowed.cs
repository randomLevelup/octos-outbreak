using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isShadowed : MonoBehaviour
{
    public bool shadowed = false;
    public Transform lightSource;
    public LayerMask layersToHit;
    public AudioSource discoveredSFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (lightSource.position - transform.position).normalized;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, direction, float.MaxValue, layersToHit);
        if(raycastHit2D){
            if(raycastHit2D.collider.gameObject.GetComponent("ShadowCaster2D") != null){
                shadowed = true;
            }
            else{
                //if you were shadowed and walked out play alert sound 
                if(shadowed){
                    Debug.Log("snake?!");
                    discoveredSFX.Play();
                }
                shadowed = false;   
            }
        }
        else {
            if(shadowed){
                Debug.Log("snake?!");
                discoveredSFX.Play();
            }
            shadowed = false;
        }
    }
}
