using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagHandler : MonoBehaviour
{
    // Start is called before the first frame update
    // public int numScenes = 4;
    public static int curScene = 0;

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nextGame();
        }
    }
    public void nextGame()
    {
        curScene++;
        // curScene = curScene % numScenes;
        
        SceneManager.LoadScene(curScene);
    }
}
