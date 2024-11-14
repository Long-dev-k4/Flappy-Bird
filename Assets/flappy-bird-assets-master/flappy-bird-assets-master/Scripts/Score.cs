using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0; // Khởi tạo điểm số thành 0
    public Text text;     // Thành phần văn bản để hiển thị điểm
    public Text highScoreText; // Thành phần văn bản để hiển thị điểm cao

    private int highScore; // Biến để lưu trữ điểm cao

    private void Start()
    {
        score = 0; // Khởi tạo điểm số

        // Tải điểm số cao đã lưu từ PlayerPrefs
        highScore = PlayerPrefs.GetInt("Điểm cao nhất:", 0);

        // Kiểm tra xem thành phần văn bản có được gán không
        if (text != null)
        {
            // Thiết lập hiển thị điểm ban đầu
            text.text = score.ToString();
        }

        // Kiểm tra xem thành phần văn bản có điểm cao đã được gán chưa
        if (highScoreText != null)
        {
            // Thiết lập hiển thị điểm cao ban đầu
            highScoreText.text = "Điểm cao nhất:" + highScore.ToString();
        }
        
    }

    public void addScore()
    {
        // Tăng điểm và cập nhật UI
        score++;
        if (text != null)
        {
            text.text = score.ToString();
        }

        // Kiểm tra xem điểm hiện tại có lớn hơn điểm cao không
        if (score > highScore)
        {
            highScore = score; // Cập nhật điểm cao
            PlayerPrefs.SetInt("Điểm cao nhất:", highScore); // Lưu điểm cao mới

            // Cập nhật hiển thị điểm cao
            if (highScoreText != null)
            {
                highScoreText.text = "Điểm cao nhất:" + highScore.ToString();
            }
        }
    }

    public int getScore()
    {
        return score; // Trả về điểm số hiện tại
    }

    public int getHighScore()
    {
        return highScore; // Trả về điểm cao nhất
    }
}
