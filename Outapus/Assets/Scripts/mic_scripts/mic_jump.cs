using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mic_jump : MonoBehaviour
{
    [SerializeField] float jump = 10;
    public Rigidbody2D controlBody;
    public SpriteRenderer controlSprite;
    private rbIsGrounded controlState;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (controlBody == null) { controlBody = GetComponent<Rigidbody2D>(); }
        if (controlSprite == null) { controlSprite = GetComponent<SpriteRenderer>(); }
        controlState = controlBody.GetComponent<rbIsGrounded>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controlState.isGrounded) {
            controlSprite.color = new Color (50,100,0);
        }
        else {
            controlSprite.color = new Color (100,100,100);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if(controlState.isGrounded) {
                float tempJump = 10;
                if (controlState.jumpPower) {
                    tempJump = 15;
                    controlState.jumpPower = false;
                }
                controlBody.AddForce(Vector2.up * tempJump, ForceMode2D.Impulse);
                audioSource.Play();
            }
        }
    }
}
