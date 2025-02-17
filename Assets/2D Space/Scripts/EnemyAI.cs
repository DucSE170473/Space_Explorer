using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public bool roaming = true;
    public float moveSpeed = 3f;
    public float roamDistance = 5f;
    public float chaseRange = 10f;

    private Vector2 startPos;
    private Vector2 targetPosition;
    private Transform player;
    private bool movingRight = true;

    //Shooting

    public bool isShootable = true;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float timeBtwFire;

    private float fireCooldown;

    private void Start()
    {
        startPos = transform.position;
        player = FindObjectOfType<PlayerController>()?.transform;

        if (roaming)
            SetNextRoamingTarget();
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0)
        {
            fireCooldown = timeBtwFire;
            //shoot
            EnemyFireBullet();
        }

        if (!roaming && player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer < chaseRange)
            {
                targetPosition = player.position;
            }
        }

        MoveToTarget();
    }

    void EnemyFireBullet()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector3 playerPos = FindAnyObjectByType<PlayerController>().transform.position;
        Vector3 direction = playerPos - transform.position;
        rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
    }

    void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Nếu enemy đã đến điểm mục tiêu, đổi hướng
        if ((Vector2)transform.position == targetPosition && roaming)
        {
            movingRight = !movingRight; // Đổi hướng
            SetNextRoamingTarget();
        }
    }

    void SetNextRoamingTarget()
    {
        if (movingRight)
            targetPosition = startPos + new Vector2(roamDistance, 0); // Di chuyển sang phải
        else
            targetPosition = startPos - new Vector2(roamDistance, 0); // Di chuyển sang trái
    }
}
