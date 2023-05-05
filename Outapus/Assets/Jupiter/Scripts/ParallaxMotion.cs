using UnityEngine;

[System.Serializable]
public class LayerPair
{
    public Transform obj;
    public float depth;
    private Vector3 basePos;

    public void SetBasePos() { basePos = obj.position; }
    public void UpdateLayer(Vector3 diff) { obj.position = basePos + (diff / depth); }
}

public class ParallaxMotion : MonoBehaviour
{
    public Camera cam;
    public LayerPair[] layers;

    private Vector3 camBasePos;

    private void Start()
    {
        camBasePos = cam.transform.position;
        foreach (LayerPair layer in layers) { layer.SetBasePos(); }
    }

    private void FixedUpdate()
    {
        Vector3 camDiff = cam.transform.position - camBasePos;
        foreach (LayerPair layer in layers) { layer.UpdateLayer(camDiff); }
    }
}