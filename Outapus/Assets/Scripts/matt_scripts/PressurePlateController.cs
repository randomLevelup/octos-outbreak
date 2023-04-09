using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    private bool isActive;
    private int objectsOnPlate;

    void Start()
    {
        isActive = false;
        objectsOnPlate = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        objectsOnPlate++;
        Debug.Log("object enterning pressure plate");
        //if (collision.gameObject.CompareTag("Player"))
        //{
            // Debug.Log("player enterning pressure plate");
            isActive = true;
        //}
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // if (collision.gameObject.CompareTag("Player"))
        // {
        //     isActive = false;
        // }

        objectsOnPlate--;
        if (objectsOnPlate <= 0)
        {
            isActive = false;
            objectsOnPlate = 0;
        }
    }

    public bool IsActive()
    {
        return isActive;
    }
}
