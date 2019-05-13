using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuStart : MonoBehaviour
{
    public void ChangeMenuScene(string Scene)
    {
        // Breytir um scene
        SceneManager.LoadScene(Scene);
    }
    public void QuitGame()
    {
        // Stoppar applicationið(Leikinn)
        Application.Quit();
    }
}
