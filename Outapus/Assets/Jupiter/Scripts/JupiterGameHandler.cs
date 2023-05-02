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
    public GameObject[] NPC;

    private void Start() { currentLevelIndex = 0; InitializeLevel(); }

    public void InitializeLevel()
    {
        

        LevelInterface curLevel = levels[currentLevelIndex].GetComponent<LevelInterface>();

<<<<<<< HEAD
        NPC = new GameObject[curLevel.NPCsSpawnPoint.Length];

        //destroy NPC's from previous scene
        for(int i = 0; i < curLevel.NPCsSpawnPoint.Length; i++){
            Destroy(NPC[i]);
        }

        GameObject octoBody = Instantiate(octoBodyPrefab,
                                          curLevel.spawnPoint.transform.position,
                                          Quaternion.identity);
        octoBody.GetComponent<isShadowed>().lightSource = curLevel.lightSource;
        Transform abdomen = octoBody.GetComponent<FeetController>().abdomenObject;
=======
        GameObject octoBody = Instantiate(octoBodyPrefab, curLevel.spawnPoint.transform.position, Quaternion.identity);
        OctoAnimator octoAnim = octoBody.GetComponent<OctoAnimator>();
>>>>>>> 887e654b65b2b3a75901b700a21173bdedb124b6

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
<<<<<<< HEAD
            NPC[i].GetComponent<GroundNPCMovement>().player = octoBody;
=======
            NPC.GetComponent<GroundNPCMovement>().player = octoAnim.abdomenObject;
>>>>>>> 887e654b65b2b3a75901b700a21173bdedb124b6
        }
        
    }
}
