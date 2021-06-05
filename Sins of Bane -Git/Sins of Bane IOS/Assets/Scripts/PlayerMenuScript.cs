using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenuScript : MonoBehaviour
{
    public GameObject PlayerMenu;

    public GameObject Credits;

    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        //Canvas.GetComponent<CanvasGroup>().alpha = 0f;
        Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Pause();
        }
    }

    public void Pause()
    {
        Canvas.SetActive(true);
        Canvas.GetComponent<CanvasGroup>().alpha = 0.8f;
    }

    public void Resume()
    {
        Canvas.SetActive(false);
    }

    public void CreditsMenu()
    {
        PlayerMenu.SetActive(false);
        Credits.SetActive(true);
    }

    public void BackToPlayerMenu()
    {
        PlayerMenu.SetActive(true);
        Credits.SetActive(false);
    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
    }
}
