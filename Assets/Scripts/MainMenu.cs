using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lvl1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
