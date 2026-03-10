using UnityEngine;

public class BattleFlow : MonoBehaviour
{
    [Header("Battle Flow Settings")]
    
    [Tooltip("Kéo CanvasGameOver từ Hierarchy vào đây")]
    public GameObject gameOverUI;

    [Tooltip("Kéo object Player có chứa script PlayerHealth vào đây")]
    public PlayerHealth playerHealth; 

    [Tooltip("Kéo object chứa AudioSource nhạc nền vào đây")]
    public AudioSource bgMusic;
    
    [Tooltip("Kéo CanvasGameWin từ Hierarchy vào đây")]
    public GameObject gameWinUI;

    private bool isGameOver = false;

    void Start()
    {
        // Đảm bảo cả hai màn hình UI bị ẩn khi mới bắt đầu game
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (gameWinUI != null) gameWinUI.SetActive(false);
    }

    void Update()
    {
        // 1. Kiểm tra điều kiện thua (Mất hết máu)
        if (!isGameOver && playerHealth != null && playerHealth.currentHealth <= 0) 
        {
            GameOver();
        }

        // 2. Kiểm tra điều kiện thắng (Tiêu diệt hết quái vật)
        if (!isGameOver)
        {
            // Tìm tất cả các object đang có Tag là "Enemy" trên màn hình
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            // Nếu không còn object "Enemy" nào (độ dài mảng = 0) thì Win
            if (enemies.Length == 0)
            {
                GameWin();
            }
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        if (bgMusic != null && bgMusic.isPlaying)
        {
            bgMusic.Stop();
        }
    }

    public void GameWin()
    {
        isGameOver = true;

        if (gameWinUI != null) gameWinUI.SetActive(true);
        
        if (bgMusic != null && bgMusic.isPlaying) bgMusic.Stop();

        Debug.Log("Chiến thắng rồi!");
    }
}