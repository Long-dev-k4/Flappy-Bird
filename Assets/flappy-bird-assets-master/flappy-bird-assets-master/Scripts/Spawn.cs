using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Tham chiếu đến GameObject "pipe" sẽ được sinh ra
    public GameObject pipe;

    // Thời gian tối đa giữa mỗi lần sinh đối tượng
    public float maxTime;

    // Biến đếm thời gian để theo dõi thời gian đã trôi qua từ lần sinh đối tượng cuối cùng
    float timer;

    // Kiểm soát phạm vi vị trí theo chiều dọc mà đối tượng sẽ được sinh ra
    public float height;

    // Khởi tạo biến đếm thời gian bằng maxTime khi trò chơi bắt đầu
    private void Start()
    {
        timer = maxTime;
    }

    private void Update()
    {
        // Kiểm tra nếu biến đếm thời gian vượt quá maxTime, nghĩa là đã đến lúc sinh đối tượng mới
        if (timer > maxTime)
        {
            // Tạo một đối tượng "pipe" mới tại một vị trí ngẫu nhiên theo chiều dọc trong phạm vi height
            GameObject tmp = Instantiate(pipe, new Vector3(transform.position.x, transform.position.y + Random.Range(-height, height), 0), Quaternion.identity);

            // Hủy đối tượng sau 10 giây để tránh làm đầy cảnh trong trò chơi
            Destroy(tmp, 10f);

            // Đặt lại biến đếm thời gian sau khi sinh đối tượng
            timer = 0;
        }

        // Tăng biến đếm thời gian theo thời gian đã trôi qua từ khung hình trước
        timer += Time.deltaTime;
    }
}
