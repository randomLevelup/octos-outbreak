using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isShadowed : MonoBehaviour
{
    public bool shadowed;
    public Transform lightSource;
    public LayerMask layersToHit;
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
                shadowed = false;
            }
        }
        else {
            shadowed = false;
        }
    }
}
