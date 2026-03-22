using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    [Header("Cài đặt hiển thị")]
    public Color pathColor = Color.green; 
    public float waypointSize = 0.2f;     // Kích thước cục tròn tại mỗi điểm

    // Hàm này tự động chạy trong cửa sổ Scene (không cần ấn Play)
    private void OnDrawGizmos()
    {
        Gizmos.color = pathColor;

        // Lặp qua tất cả các điểm (con) nằm trong Path_01
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform currentWaypoint = transform.GetChild(i);
            
            // Vẽ một hình cầu nhỏ đánh dấu vị trí điểm Waypoint
            Gizmos.DrawSphere(currentWaypoint.position, waypointSize);

            // Nếu chưa phải là điểm cuối cùng, vẽ một đoạn thẳng nối sang điểm tiếp theo
            if (i < transform.childCount - 1)
            {
                Transform nextWaypoint = transform.GetChild(i + 1);
                Gizmos.DrawLine(currentWaypoint.position, nextWaypoint.position);
            }
        }
    }
}