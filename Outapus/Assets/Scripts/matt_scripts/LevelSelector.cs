using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public int levelNumber;
    //public string levelName;
    public Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        levelText.text = levelNumber.ToString();
    }

    public void OpenScene(){
        StaticVariables.currentLevelIndex = levelNumber - 1;
        Debug.Log(StaticVariables.currentLevelIndex - 1);
        SceneManager.LoadScene("week-15-dev");

        // SceneManager.LoadScene(levelNumber);
        //SceneManager.LoadScene("Level " + levelNumber.ToString());
        //SceneManager.LoadScene("(1)Tutorial_lvl");
    }

}
