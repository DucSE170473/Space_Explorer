using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    public GameObject pauseMenuUI; // Kéo Panel Pause Menu vào đây

    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(ResumeGame);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Tiếp tục game
        pauseMenuUI.SetActive(false); // Ẩn Pause Menu
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("▶ Tiếp tục game");
    }
}
