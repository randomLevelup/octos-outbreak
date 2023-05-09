using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.transform.parent.gameObject);

            JupiterGameHandler gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<JupiterGameHandler>();
            //gameHandler.currentLevelIndex++;
            StaticVariables.currentLevelIndex++;
            gameHandler.DestroyClones();
            gameHandler.InitializeLevel();
        }
    }
}
