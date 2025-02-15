using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SceneLevel_1");
    }

    public void GuideGame()
    {
        SceneManager.LoadScene("SceneGuide");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
