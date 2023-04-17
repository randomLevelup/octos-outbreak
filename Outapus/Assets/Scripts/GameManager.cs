using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;

    void Awake (){
            SetLevel (volumeLevel);
            GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
            if (sliderTemp != null){
                    sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                    sliderVolumeCtrl.value = volumeLevel;
            }
    }

    // Start is called before the first frame update
    void Start (){
            pauseMenuUI.SetActive(false);
            GameisPaused = false;
    }

    // Update is called once per frame
    void Update (){
            if (Input.GetKeyDown(KeyCode.Escape)){
                    if (GameisPaused){
                            Resume();
                    }
                    else{
                            Pause();
                    }
            }
    }

    void Pause(){
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameisPaused = true;
    }

    public void Resume(){
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameisPaused = false;
    }

    public void SetLevel (float sliderValue){
            mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
            volumeLevel = sliderValue;
    }

    public void RestartGame(){
        Time.timeScale = 1f;
        // Add commands to zero-out any scores or other stats before restarting
        // SceneManager.LoadScene("MainMenu");
        // Please also reset all static variables here, for new games!
    }

    public void QuitGame(){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
    }
}
