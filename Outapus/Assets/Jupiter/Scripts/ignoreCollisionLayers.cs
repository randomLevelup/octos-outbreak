using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ignoreCollisionLayers : MonoBehaviour
{
    public int[] layersToIgnore = { 6, 8 };

    private void Start()
    {
        int thisLayer = gameObject.layer;
        foreach (int i in layersToIgnore)
        {
            Physics2D.IgnoreLayerCollision(thisLayer, i);
        }
    }
}
