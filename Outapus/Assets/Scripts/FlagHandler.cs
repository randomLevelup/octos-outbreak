using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagHandler : MonoBehaviour
{
    // Start is called before the first frame update
    // public int numScenes = 4;
    public static int curScene = 1;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nextGame();
        }
    }
    public void nextGame()
    {
        audioSource.volume = JupiterGameHandler.volumeLevel;
        audioSource.Play();
        curScene++;
        // curScene = curScene % numScenes;
        SceneManager.LoadScene(curScene);
    }
}
