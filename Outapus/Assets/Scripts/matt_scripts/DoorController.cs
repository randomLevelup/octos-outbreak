using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject pressurePlate;
    private bool isDoorOpen;
    private SpriteRenderer spriteRenderer;
    private Color doorColorOpen = new Color(0.56f, 0.93f, 0.56f, 1f);
    private Color doorColorClosed = new Color(0.56f, 0.93f, 0.56f, 0f);

    void Start()
    {
        isDoorOpen = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PressurePlateController pressurePlateController = pressurePlate.GetComponent<PressurePlateController>();
        isDoorOpen = pressurePlateController.IsActive();

        if (isDoorOpen)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            spriteRenderer.color = doorColorClosed; // Set opacity to 0
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            spriteRenderer.color = doorColorOpen; // Set opacity to 1
        }
    }
}
