using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlappyBird : MonoBehaviour
{
    // Tham chiếu doi tuong
    private Rigidbody2D rg;
    public GameObject gameOverObj;
    public GameObject message;
    public GameObject scoreend;
    public GameObject highscore; 
    public float speed;
    private bool isGameStarted = false;
    private bool isGameOver = false;

    private Score scoreComponent;
    private BackGround backgroundComponent; // Tham chiếu đến tập lệnh BackGround

    private static bool isThemeMusicPlaying = false; // Biến tĩnh để theo dõi nhạc chủ đề
    private Coroutine themeMusicCoroutine; // Để lưu trữ phiên bản coroutine

    private void Start()
    {
        Time.timeScale = 0; // Tạm dừng trò chơi khi bắt đầu
        rg = GetComponent<Rigidbody2D>();
        message.GetComponent<SpriteRenderer>().enabled = true;

        // Tìm các thành phần Score và BackGround trong cảnh
        scoreComponent = FindObjectOfType<Score>();
        backgroundComponent = FindObjectOfType<BackGround>();

        // Chỉ bắt đầu nhạc chủ đề nếu nó chưa được phát
        if (!isThemeMusicPlaying)
        {
            themeMusicCoroutine = StartCoroutine(PlayThemeMusicWithPause());
            isThemeMusicPlaying = true; // Đặt cờ thành true sau khi bắt đầu chủ đề
        }
    }

    private IEnumerator PlayThemeMusicWithPause()
    {
        while (!isGameOver)
        {
            SoundController.instance.PlayThisSound("theme", 38f);
            yield return new WaitForSeconds(38f + 3f); // Đợi thời lượng nhạc cộng thêm 3 giây
        }
    }

    private void Update()
    {
        if (isGameOver) return; // Dừng tất cả các hành động nếu trò chơi kết thúc

        if (!isGameStarted && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            isGameStarted = true;
            Time.timeScale = 1; // Bắt đầu trò chơi
            SoundController.instance.PlayThisSound("wing", 0.5f);
            rg.AddForce(Vector2.up * speed);
            message.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (isGameStarted && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            SoundController.instance.PlayThisSound("wing", 0.5f);
            rg.AddForce(Vector2.up * speed);
        }
    }

    // Reset Game
    public void resetGame()
    {
        SceneManager.LoadScene(0);
    }

    // Thoat khoi ung dung
    public void gameExit()
    {
        Application.Quit();
    }

    // Tham chiếu đến tập lệnh PauseStart
    [SerializeField] private PauseStart pauseStartScript;

    void GameOver()
    {
        SoundController.instance.PlayThisSound("loser", 1f);

        // Kích hoạt đối tượng Game Over
        gameOverObj.SetActive(true);
        isGameOver = true; // Đặt cờ kết thúc trò chơi

        // Dừng chương trình coroutine nhạc chủ đề
        if (themeMusicCoroutine != null)
        {
            StopCoroutine(themeMusicCoroutine);
            isThemeMusicPlaying = false; // Đặt lại cờ
        }

        // Hiển thị đối tượng scoreend và cập nhật văn bản
        scoreend.SetActive(true);
        Text scoreText = scoreend.GetComponent<Text>();
        if (scoreText != null && scoreComponent != null)
        {
            scoreText.text = "Điểm của bạn là:" + scoreComponent.getScore().ToString();
        }

        // Hiển thị điểm cao
        highscore.SetActive(true);
        Text highScoreText = highscore.GetComponent<Text>();
        if (highScoreText != null && scoreComponent != null)
        {
            highScoreText.text = "Điểm cao nhất:" + scoreComponent.getHighScore().ToString();
        }

        // Thiết lập thứ tự hiển thị cho đối tượng Game Over
        var spriteRenderer = gameOverObj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = 10; // Đặt giá trị cao cho mức độ ưu tiên hiển thị
        }

        // Đảm bảo Giao diện người dùng Game Over hiển thị
        var canvas = gameOverObj.GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        // Dừng chuyển động nền
        if (backgroundComponent != null)
        {
            backgroundComponent.GameOver();
        }

        // Ẩn nút Tạm dừng
        if (pauseStartScript != null)
        {
            pauseStartScript.HideButton();
        }

        // Dừng thời gian trò chơi
        Time.timeScale = 0;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGameOver)
        {
            SoundController.instance.PlayThisSound("hit", 0.5f);
            GameOver();
        }
    }
}
