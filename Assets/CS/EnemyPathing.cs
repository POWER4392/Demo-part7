using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [Header("Tên đường bay trong Scene")]
    // Tên của GameObject chứa các điểm Waypoint ngoài Scene
    public string pathName = "Path_01"; 
    
    public float moveSpeed = 5f;
    
    // Đổi thành private vì nó sẽ tự động tìm, không cần kéo thả nữa
    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = 0;

    void Start()
    {
        // Khi vừa sinh ra, tự động tìm cái GameObject tên là "Path_01" (hoặc tên bạn đặt)
        GameObject pathObject = GameObject.Find(pathName);
        
        if (pathObject != null)
        {
            // Lấy tất cả các cục Waypoint con nằm bên trong Path_01 cho vào danh sách
            foreach (Transform child in pathObject.transform)
            {
                waypoints.Add(child);
            }
        }
        else
        {
            Debug.LogWarning("Không tìm thấy đường bay nào tên là: " + pathName);
        }
    }

    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        // Nếu không có waypoint nào thì không làm gì cả để tránh lỗi
        if (waypoints.Count == 0) return; 

        // Kiểm tra xem có còn waypoint nào để đi tới không
        if (currentWaypointIndex < waypoints.Count)
        {
            Vector2 targetPosition = waypoints[currentWaypointIndex].position;
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

            // Nếu đã đến gần waypoint hiện tại, chuyển mục tiêu sang waypoint tiếp theo
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            // Hủy object kẻ địch nếu nó đã bay đến điểm Waypoint cuối cùng
            Destroy(gameObject); 
        }
    }
}