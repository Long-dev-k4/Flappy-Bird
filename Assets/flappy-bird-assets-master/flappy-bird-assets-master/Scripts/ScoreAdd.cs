using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Phát âm thanh "point" khi người chơi đi qua chướng ngại vật
        SoundController.instance.PlayThisSound("point", 0.5f);

        // Truy cập và cập nhật điểm trong thành phần Điểm
        FindObjectOfType<Score>().addScore();
    }
}
