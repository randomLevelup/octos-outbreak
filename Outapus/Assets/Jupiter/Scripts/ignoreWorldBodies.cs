using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ignoreWorldBodies : MonoBehaviour
{
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 6);
        Physics2D.IgnoreLayerCollision(7, 8);
    }
}
