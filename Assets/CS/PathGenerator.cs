using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [Header("Cài đặt đường lượn sóng")]
    public int numberOfPoints = 20;     // Tổng số điểm Waypoint sẽ tạo ra
    public float spacingY = 0.5f;       // Khoảng cách đi xuống giữa mỗi điểm
    public float waveWidth = 3f;        // Biên độ sóng (bay lượn sang 2 bên bao xa)
    public float waveFrequency = 0.5f;  // Tần số sóng (lượn gắt hay lượn thoải)

    // Nút bấm ảo để xóa các điểm cũ
    [ContextMenu("1. Xóa đường bay cũ")]
    public void ClearPath()
    {
        // Xóa ngược từ dưới lên để không bị lỗi index trong Unity
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    // Nút bấm ảo để tự động sinh ra đường lượn sóng
    [ContextMenu("2. Tạo đường lượn sóng")]
    public void GenerateSineWave()
    {
        ClearPath(); // Xóa sạch điểm cũ trước khi tạo

        Vector2 startPos = transform.position; // Vị trí bắt đầu là vị trí của Path_01

        for (int i = 0; i < numberOfPoints; i++)
        {
            // Tạo 1 GameObject trống mới
            GameObject newPoint = new GameObject("Điểm " + (i + 1));
            // Cho nó làm con của Path_01
            newPoint.transform.SetParent(transform);

            // Tính toán tọa độ lượn sóng (Toán học lượng giác Sin)
            float posY = startPos.y - (i * spacingY); 
            float posX = startPos.x + Mathf.Sin(i * waveFrequency) * waveWidth; 

            // Áp dụng tọa độ
            newPoint.transform.position = new Vector3(posX, posY, 0);
        }
    }
}