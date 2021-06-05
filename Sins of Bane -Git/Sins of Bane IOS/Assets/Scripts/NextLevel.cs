using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public AudioSource audioData;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /*void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }*/

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        audioData = GetComponent<AudioSource>();
        audioData.Play();
    }
}
