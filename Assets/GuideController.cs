using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideController : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
