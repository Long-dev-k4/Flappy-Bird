using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseStart : MonoBehaviour
{
    [SerializeField] private Image pauseIcon; // Pause icon
    [SerializeField] private Image startIcon; // Start icon
    [SerializeField] private Button pauseButton; // Nút kích hoạt chức năng tạm dừng
    private bool isPaused = false; // Trạng thái ban đầu

    void Start()
    {
        UpdateButtonIcon(); // Đặt biểu tượng ban đầu
        pauseButton.onClick.AddListener(TogglePause); // su kien cho button
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        // Tạm dừng hoặc tiếp tục trò chơi dựa trên isPaused
        Time.timeScale = isPaused ? 0 : 1;

        UpdateButtonIcon(); // Cập nhật biểu tượng khi trạng thái thay đổi
    }

    private void UpdateButtonIcon()
    {
        // Hiển thị biểu tượng phù hợp dựa trên trạng thái tạm dừng
        pauseIcon.enabled = !isPaused;
        startIcon.enabled = isPaused;
    }

    public void HideButton()
    {
        // Ẩn nút tạm dừng
        pauseButton.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        pauseButton.onClick.RemoveListener(TogglePause); // Dọn dẹp trình lắng nghe
    }
}
