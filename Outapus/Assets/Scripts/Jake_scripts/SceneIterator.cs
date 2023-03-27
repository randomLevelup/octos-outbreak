using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneIterator : MonoBehaviour
{
    public static int curScene = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.N)){
            nextGame();
        }
    }

    public void nextGame(){
        curScene++;
        SceneManager.LoadScene(curScene);
    }
}
