using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject pressurePlate;
    private bool isDoorOpen;
    private SpriteRenderer spriteRenderer;

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
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f); // Set opacity to 0
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Set opacity to 1
        }
    }
}
