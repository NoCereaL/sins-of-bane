using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreditsMenu;
    
    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
    }
    
    public void PlayNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        audioData = GetComponent<AudioSource>();
        audioData.Play();
    }

    public void CreditsButton() 
    {
        // Show Credits Menu
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        audioData = GetComponent<AudioSource>();
        audioData.Play();
    }

    public void MainMenuButton()
    {
        // Show Main Menu
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        audioData = GetComponent<AudioSource>();
        audioData.Play();
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}
