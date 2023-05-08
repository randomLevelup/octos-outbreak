using UnityEngine;

[System.Serializable]
public class LayerPair
{
    public Transform obj;
    public float depth;
    private Vector3 basePos;

    public void SetBasePos() { basePos = obj.localPosition; }
    public void UpdateLayer(Vector3 diff) { obj.localPosition = basePos + (diff / depth); }
}

public class ParallaxMotion : MonoBehaviour
{
    public Camera cam;
    public LayerPair[] layers;

    private Vector3 camBasePos;

    public void InitializeCamera(Transform levelBasePos)
    {
        camBasePos = levelBasePos.position;
        foreach (LayerPair layer in layers) { layer.SetBasePos(); }
    }

    private void Start() { InitializeCamera(Camera.main.transform); }

    private void FixedUpdate()
    {
        Vector3 camDiff = cam.transform.position - camBasePos;
        foreach (LayerPair layer in layers) { layer.UpdateLayer(camDiff); }
    }
}