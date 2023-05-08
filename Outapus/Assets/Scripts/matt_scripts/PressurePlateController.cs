using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    private bool isActive;
    private int objectsOnPlate;
    public GameObject plate;
    private float yPos;

    void Start()
    {
        isActive = false;
        objectsOnPlate = 0;
        yPos = transform.position.y;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        objectsOnPlate++;
        Debug.Log("object enterning pressure plate");
        isActive = true;
        transform.position = new Vector3(transform.position.x, yPos-0.0f, transform.position.z);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        objectsOnPlate--;
        if (objectsOnPlate <= 0)
        {
            isActive = false;
            objectsOnPlate = 0;
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }
    }

    public bool IsActive()
    {
        return isActive;
    }
}
