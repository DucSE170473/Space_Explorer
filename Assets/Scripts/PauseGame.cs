using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI; // Kéo Panel vào đây

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Nhấn ESC để bật/tắt Pause
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused); // Hiển thị/tắt UI

        if (isPaused)
        {
            Time.timeScale = 0;
            Debug.Log("⏸ Game đã tạm dừng");
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("▶ Game tiếp tục");
        }
    }

    public void QuitGame()
    {
        Debug.Log("🚪 Thoát game");
        Application.Quit(); // Thoát game
    }
}
