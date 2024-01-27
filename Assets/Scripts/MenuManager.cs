using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditMenu;

    private bool mainMenuOpen;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        creditMenu.SetActive(false);

        mainMenuOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method for leaving the game
    public void Exit()
    {
        Application.Quit();
    }

    //method that starts the game
    public void Play()
    {

    }

    public void Credit()
    {
        mainMenu.SetActive(!mainMenuOpen);
        creditMenu.SetActive(mainMenuOpen);

        mainMenuOpen = !mainMenuOpen;
    }


}
