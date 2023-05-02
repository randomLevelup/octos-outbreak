using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JupiterGameHandler : MonoBehaviour
{
    public int currentLevelIndex;

    public GameObject[] levels;
    public GameObject octoBodyPrefab;
    public Camera mainCam;
    public LineRenderer lineRenderer;
    public GameObject NPCPrefab;
    private GameObject[] NPC;

    private void Start() { currentLevelIndex = 0; InitializeLevel(); }

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
