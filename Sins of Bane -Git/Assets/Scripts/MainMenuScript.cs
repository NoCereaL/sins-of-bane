using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreditsMenu;
    
    public AudioSource audioData;
    public AudioSource audioBack;

    // Start is called before the first frame update
    void Start()
    {
    }
    
    public void PlayNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        audioData.Play();
    }

    public void CreditsButton() 
    {
        // Show Credits Menu
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        audioData.Play();
    }

    public void MainMenuButton()
    {
        // Show Main Menu
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        audioBack.Play();
    }

    public void QuitButton()
    {
        audioData.Play();
        Application.Quit();
    }

}
