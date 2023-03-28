using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneIterator : MonoBehaviour
{
    public int numScenes = 5;
    public static int curScene = 0;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.N)){
            nextGame();
        }
    }

    public void nextGame(){
        curScene++;
        if (curScene == numScenes) { curScene= 0; }

        SceneManager.LoadScene(curScene);
    }
}
