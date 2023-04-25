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

    private void Start() { currentLevelIndex = 0; InitializeLevel(); }

    public void InitializeLevel()
    {
        LevelInterface curLevel = levels[currentLevelIndex].GetComponent<LevelInterface>();

        GameObject octoBody = Instantiate(octoBodyPrefab,
                                          curLevel.spawnPoint.transform.position,
                                          Quaternion.identity);
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
            GameObject NPC = Instantiate(NPCPrefab, curLevel.NPCsSpawnPoint[i].transform.position,
            Quaternion.identity);
            NPC.GetComponent<GroundNPCMovement>().player = abdomen;
        }
        
    }
}
