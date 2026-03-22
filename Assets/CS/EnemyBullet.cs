using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float flySpeed = 5f;
    public int damage = 1;
    public float lifeTime = 3f; 

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Space.World giúp đạn luôn bay xuống dưới bất chấp việc bị xoay hướng
        transform.Translate(Vector2.down * flySpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerHealth>();
        
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject); 
            return;
        }

        if (collision.CompareTag("Ground") || collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}