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

        LineController lineController = lineRenderer.GetComponent<LineController>();
        lineController.player = abdomen;
        lineController.hinge = curLevel.hingeFolder.GetComponentsInChildren<SpringJoint2D>()[0].transform;

        StaticSwingHandler swingHandler = octoBody.GetComponent<StaticSwingHandler>();
        swingHandler.hingeFolder = curLevel.hingeFolder;
        swingHandler.lr = lineRenderer;
        swingHandler.SwingController = lineRenderer.gameObject;

        CameraController camController = mainCam.GetComponent<CameraController>();
        camController.abdomen = abdomen;
        camController.bounds = curLevel.cameraBounds;
        camController.InitBounds();
       
        //set The NPC's to target 
        for(int i = 0; i < curLevel.NPCsSpawnPoint.Length; i++){
            NPC[i] = Instantiate(NPCPrefab, curLevel.NPCsSpawnPoint[i].transform.position,
            Quaternion.identity);
            NPC[i].GetComponent<GroundNPCMovement>().player = octoBody;
        }
        
    }
}
