using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public Transform target;           // 目標のTransform
    public GameObject bulletPrefab;    // 弾のPrefab
    public float bulletSpeed = 10f;    // 弾速（固定）
    GameObject bullet;
    Rigidbody2D targetRb;
    Rigidbody2D bulletRb;

    void Awake()
    {
        targetRb = target.GetComponent<Rigidbody2D>();
        bulletRb = bullet.GetComponent<Rigidbody2D>();
        InvokeRepeating("Shoot", 1f, 1f);
    }

    void Update()
    {
        
    }

    void Shoot()
    {
        Vector2 shooterPos = transform.position;
        Vector2 targetPos = target.position;
        Vector2 targetVelocity = targetRb.linearVelocity;

        // 相対位置と速度
        Vector2 toTarget = targetPos - shooterPos;

        // 予測時間を計算
        float a = Vector2.Dot(targetVelocity, targetVelocity) - bulletSpeed * bulletSpeed;
        float b = 2 * Vector2.Dot(toTarget, targetVelocity);
        float c = Vector2.Dot(toTarget, toTarget);

        float discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
        {
            // 命中不可（弾速が遅すぎるなど）
            return;
        }

        float t1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        float t2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
        float t = Mathf.Max(t1, t2);

        // 予測位置
        Vector2 aimPoint = targetPos + targetVelocity * t;

        // 弾を発射
        bullet = Instantiate(bulletPrefab, shooterPos, Quaternion.identity);
        Vector2 direction = (aimPoint - shooterPos).normalized;
        bulletRb.linearVelocity = direction * bulletSpeed;

        Debug.Log("弾を発射しました");
    }
}
