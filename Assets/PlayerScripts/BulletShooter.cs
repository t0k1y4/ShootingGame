using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    AudioSource shotSound;          //打つ時の音
    public string targetTag = "Enemy";     // ターゲットのタグ名
    public GameObject bulletPrefab;         // 弾のPrefab
    public float bulletSpeed = 10f;         // 弾速（固定）
    public float time = 0;
    public float maxRate = 1f;
    public float rate = 1f;
    public float a = 0.1f;
    GameObject bulletController;
    BulletController bulletScripts;

    private GameObject[] targets;

    void Awake()
    {
        rate = maxRate;
        shotSound = GetComponent<AudioSource>();       //打つ時の音
        bulletScripts = bulletPrefab.GetComponent<BulletController>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > rate)
        {
            ShootClosestTarget();
            time = 0;
        }
    }

    void ShootClosestTarget()
    {
        targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length == 0) return;

        GameObject closestTarget = null;
        float minDistance = Mathf.Infinity;
        Vector2 shooterPos = transform.position;

        foreach (GameObject targetObj in targets)
        {
            float dist = Vector2.Distance(shooterPos, targetObj.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestTarget = targetObj;
            }
        }

        if (closestTarget != null)
        {
            Shoot(closestTarget);
        }
    }

    void Shoot(GameObject targetObj)
    {
        if (bulletPrefab == null)
        {
            Debug.Log("null");
            return;
        }
        else if (bulletPrefab.name.Equals("HeroBullet"))
        {
            bulletScripts.damage = PlayerStats.Instance.GetPlayerDamage();

        }
        else if (bulletPrefab.name.Equals("SupporterBullet1"))
        {
            bulletScripts.damage = PlayerStats.Instance.GetSupporterDamage(1);
        }
        else if (bulletPrefab.name.Equals("SupporterBullet2"))
        {
            bulletScripts.damage = PlayerStats.Instance.GetSupporterDamage(2);
        }
        else if (bulletPrefab.name.Equals("SupporterBullet3"))
        {
            bulletScripts.damage = PlayerStats.Instance.GetSupporterDamage(3);
        }

        Rigidbody2D targetRb = targetObj.GetComponent<Rigidbody2D>();
        if (targetRb == null) return;

        Vector2 shooterPos = transform.position;
        Vector2 targetPos = targetObj.transform.position;
        Vector2 targetVelocity = targetRb.linearVelocity;

        Vector2 toTarget = targetPos - shooterPos;

        float a = Vector2.Dot(targetVelocity, targetVelocity) - bulletSpeed * bulletSpeed;
        float b = 2 * Vector2.Dot(toTarget, targetVelocity);
        float c = Vector2.Dot(toTarget, toTarget);

        float discriminant = b * b - 4 * a * c;
        if (discriminant < 0) return;

        float t1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        float t2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
        float t = Mathf.Max(t1, t2);

        Vector2 aimPoint = targetPos + targetVelocity * t;
        Vector2 direction = (aimPoint - shooterPos).normalized;

        //弾を生成する
        GameObject bullet = Instantiate(bulletPrefab, shooterPos, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = direction * bulletSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //弾のサウンドを再生
        shotSound.Play();

        Debug.Log($"最も近いターゲット {targetObj.name} に弾を発射しました");
    }

    public void ChangeRate(int x)
    {
        rate = maxRate / (a * x + 1f);
    }
}
