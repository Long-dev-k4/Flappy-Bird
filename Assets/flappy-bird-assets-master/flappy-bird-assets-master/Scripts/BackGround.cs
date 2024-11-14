using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform mainCam;
    public Transform midBg;
    public Transform sideBg;
    public float length;
    public float speed = 1.0f; // Điều chỉnh tốc độ khi cần thiết

    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return; // Dừng chuyển động nền khi trò chơi kết thúc

        // Di chuyển cả hai hình nền sang bên trái để tạo hiệu ứng cuộn
        midBg.position += Vector3.left * speed * Time.deltaTime;
        sideBg.position += Vector3.left * speed * Time.deltaTime;

        // Đảm bảo chuyển tiếp liền mạch giữa các nền mà không có khoảng trống
        if (mainCam.position.x - midBg.position.x >= length)
        {
            UpdateBackgroundPosition(midBg, sideBg);
        }
        else if (mainCam.position.x - sideBg.position.x >= length)
        {
            UpdateBackgroundPosition(sideBg, midBg);
        }
    }

    void UpdateBackgroundPosition(Transform bgToMove, Transform referenceBg)
    {
        bgToMove.position = new Vector3(referenceBg.position.x + length, referenceBg.position.y, referenceBg.position.z);
    }

    // Dừng chuyển động nền khi trò chơi kết thúc
    public void GameOver()
    {
        isGameOver = true;
    }
}
