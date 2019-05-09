using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuStart : MonoBehaviour
{
    public void ChangeMenuScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
