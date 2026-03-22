using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemyWave 
{
    public string waveName;
    public GameObject enemyPrefab;
    public int enemyCount;
    public float spawnRate;
}

public class WaveSpawner : MonoBehaviour
{
    [Header("Cài đặt Đợt Quái")]
    public EnemyWave[] waves; 
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;

    void Start()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("LỖI: Bạn chưa kéo cục SpawnPoint vào WaveSpawner!");
            return;
        }
        if (waves.Length == 0)
        {
            Debug.LogError("LỖI: Bạn chưa tạo Đợt quái (Wave) nào trong Inspector!");
            return;
        }
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        for (int w = 0; w < waves.Length; w++)
        {
            EnemyWave currentWave = waves[w]; 
            Debug.Log("--- BẮT ĐẦU: " + currentWave.waveName + " ---");

            for (int i = 0; i < currentWave.enemyCount; i++)
            {
                if (currentWave.enemyPrefab != null)
                {
                    Instantiate(currentWave.enemyPrefab, spawnPoint.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning("Cảnh báo: Ô Enemy Prefab đang bị trống ở " + currentWave.waveName);
                }
                yield return new WaitForSeconds(currentWave.spawnRate); 
            }

            Debug.Log("Hoàn thành " + currentWave.waveName + ". Đang chờ đợt tiếp theo...");
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        Debug.Log("=== CHIẾN THẮNG: ĐÃ HẾT TẤT CẢ CÁC ĐỢT QUÁI ===");
    }
}