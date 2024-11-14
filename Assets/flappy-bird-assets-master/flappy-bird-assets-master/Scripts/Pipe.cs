using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // Tốc độ di chuyển của ống
    public float speed;

    // Hàm Update được gọi mỗi khung hình
    void Update()
    {
        // Di chuyển đối tượng ống sang bên trái với tốc độ đã định
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
