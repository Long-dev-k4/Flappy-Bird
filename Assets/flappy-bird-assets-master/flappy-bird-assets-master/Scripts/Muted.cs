using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Muted : MonoBehaviour
{
    // Tham chiếu đến các icon hình ảnh "âm thanh bật" và "âm thanh tắt"
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;

    // Biến lưu trạng thái tắt tiếng, mặc định là không tắt tiếng (false)
    private bool muted = false;

    // Hàm Start được gọi khi script khởi chạy
    void Start()
    {
        // Kiểm tra nếu chưa có key "muted" trong PlayerPrefs, đặt giá trị mặc định là 0 (không tắt tiếng) và tải giá trị
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }

        // Cập nhật biểu tượng nút theo trạng thái âm thanh
        UpdateButtonIcon();

        // Cập nhật trạng thái âm thanh hệ thống theo giá trị của "muted"
        AudioListener.pause = muted;
    }

    // Hàm được gọi khi người dùng nhấn nút âm thanh
    public void OnButtonPress()
    {
        // Đổi trạng thái tắt tiếng
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true; // Tắt âm thanh
        }
        else
        {
            muted = false;
            AudioListener.pause = false; // Bật âm thanh
        }

        // Lưu trạng thái mới và cập nhật biểu tượng nút
        Save();
        UpdateButtonIcon();
    }

    // Hàm cập nhật biểu tượng nút dựa trên trạng thái tắt tiếng
    private void UpdateButtonIcon()
    {
        if (muted == false)
        {
            soundOnIcon.enabled = true; // Hiển thị biểu tượng âm thanh bật
            soundOffIcon.enabled = false; // Ẩn biểu tượng âm thanh tắt
        }
        else
        {
            soundOnIcon.enabled = false; // Ẩn biểu tượng âm thanh bật
            soundOffIcon.enabled = true; // Hiển thị biểu tượng âm thanh tắt
        }
    }

    // Hàm tải trạng thái tắt tiếng từ PlayerPrefs
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    // Hàm lưu trạng thái tắt tiếng vào PlayerPrefs
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
