using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JupiterGameHandler : MonoBehaviour
{
    public int currentLevelIndex;

    public GameObject[] levels;
    public GameObject octoBodyPrefab;
    public Camera mainCam;
    public LineRenderer lineRenderer;
    public GameObject NPCPrefab;
    private GameObject[] NPC;

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

            currentLevelIndex = 0;
            InitializeLevel();
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

    public void Pause(){
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
            Debug.Log("sliderValue = " + sliderValue);
            mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
            volumeLevel = sliderValue;
    }

    public void RestartGame(){
        //pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        // Add commands to zero-out any scores or other stats before restarting
        SceneManager.LoadScene("main_screen");
        // Please also reset all static variables here, for new games!
    }

    public void StartGame(){
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("(1)Tutorial_lvl");
    }

    public void QuitGame(){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
    }

    public void DestroyNPCs() {
        for(int i = 0; i < NPC.Length; i++){
            Destroy(NPC[i]);
        }
    }

    public void InitializeLevel()
    {


        LevelInterface curLevel = levels[currentLevelIndex].GetComponent<LevelInterface>();


        NPC = new GameObject[curLevel.NPCsSpawnPoint.Length];

        //destroy NPC's from previous scene



        GameObject octoBody = Instantiate(octoBodyPrefab,
                                          curLevel.spawnPoint.transform.position,
                                          Quaternion.identity);
        GameObject abdomen = octoBody.transform.GetChild(0).gameObject;
        //Set up the lightSource for is shadowed
        abdomen.GetComponent<isShadowed>().lightSource = curLevel.lightSource;
        Debug.Log(curLevel.lightSource);
        //GameObject octoBody = Instantiate(octoBodyPrefab, curLevel.spawnPoint.transform.position, Quaternion.identity);
        OctoAnimator octoAnim = octoBody.GetComponent<OctoAnimator>();

        EyeMovement[] eyeScriptList = octoAnim.pupilsObject.GetComponents<EyeMovement>();
        EyeMovement eyeScript = (eyeScriptList[0].enabled) ? eyeScriptList[0] : eyeScriptList[1];
        eyeScript.rb = octoAnim.abdomenObject.GetComponent<Rigidbody2D>();
        eyeScript.cam = mainCam;

        LineController lineController = lineRenderer.GetComponent<LineController>();
        lineController.player = octoAnim.abdomenObject;
        lineController.hinge = curLevel.hingeFolder.GetComponentsInChildren<SpringJoint2D>()[0].transform;

        StaticSwingHandler swingHandler = octoBody.GetComponent<StaticSwingHandler>();
        swingHandler.hingeFolder = curLevel.hingeFolder;
        swingHandler.lr = lineRenderer;
        swingHandler.SwingController = lineRenderer.gameObject;

        CameraController camController = mainCam.GetComponent<CameraController>();
        camController.abdomen = octoAnim.abdomenObject;
        camController.bounds = curLevel.cameraBounds;
        camController.InitBounds();
        //set The NPC's to target
        for(int i = 0; i < curLevel.NPCsSpawnPoint.Length; i++){
            NPC[i] = Instantiate(NPCPrefab, curLevel.NPCsSpawnPoint[i].transform.position,
            Quaternion.identity);
            NPC[i].GetComponent<GroundNPCMovement>().abdomen = abdomen;
        }
        Debug.Log("made it here");
    }
}
