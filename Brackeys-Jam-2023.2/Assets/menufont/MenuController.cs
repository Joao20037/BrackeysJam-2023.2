using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Carrega a cena de id 1
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("MenuConfig");
    }
	
    public void Main()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
