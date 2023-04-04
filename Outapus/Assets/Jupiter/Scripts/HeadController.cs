using UnityEngine;

public class HeadController : MonoBehaviour
{
    public Transform ikBase;
    public Transform ikTarget;
    public Transform bobbleHeadObject;
    public float heightOffset = 2f;

    private void Update()
    {
        transform.position = ikBase.position + new Vector3(0, heightOffset);
        ikTarget.position = bobbleHeadObject.position;
    }
}
