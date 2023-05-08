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
        yPos = plate.transform.position.y;
        Debug.Log(yPos);
        isActive = false;
        objectsOnPlate = 0;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        objectsOnPlate++;
        Debug.Log("object enterning pressure plate");
        isActive = true;
        plate.transform.position = new Vector3(plate.transform.position.x, yPos-0.1f, plate.transform.position.z);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        objectsOnPlate--;
        if (objectsOnPlate <= 0)
        {
            isActive = false;
            objectsOnPlate = 0;
            plate.transform.position = new Vector3(plate.transform.position.x, yPos, plate.transform.position.z);
        }
    }

    public bool IsActive()
    {
        return isActive;
    }
}
