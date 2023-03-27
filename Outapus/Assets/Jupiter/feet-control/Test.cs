using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Vector2?[] vecArray;

    private void Start()
    {
        vecArray = new Vector2?[2];
        vecArray[0] = Vector2.left;
        vecArray[1] = Vector2.right;

        vecArray[1] = null;
        Debug.Log(vecArray[1]);
    }
}
