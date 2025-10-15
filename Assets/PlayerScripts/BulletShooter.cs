using UnityEngine;
using System.Collections;


public class BulletShooter : MonoBehaviour
{
    AudioSource shotSound;          //打つ時の音
    public string targetTag = "Enemy";     // ターゲットのタグ名
    public GameObject bulletPrefab;         // 弾のPrefab
    public float bulletSpeed = 10f;         // 弾速（固定）
    public float angleOffset = 5f; // 上下の弾の角度（度)

    public float time = 0;
    public float defaultRate = 1f;
    public float rate = 1f;
    public float a = 0.1f;
    bool timeChenge = false;
    int supLevel1 = 0;
    int supLevel2 = 0;
    int supLevel3 = 0;
    GameObject bulletController;
    BulletController bulletScripts;

    private GameObject[] targets;

    void Awake()
    {
        rate = defaultRate;
        shotSound = GetComponent<AudioSource>();       //打つ時の音
        bulletScripts = bulletPrefab.GetComponent<BulletController>();
    }

    void Start()
    {
        ChangeStatus();
    }

    void Update()
    {
        if (Time.deltaTime == 0 && !timeChenge)
        {
            timeChenge = true;
            //Debug.Log("時間が停止しました、timeChenge:" + timeChenge);
        }
        if (Time.deltaTime != 0 && timeChenge)
        {
            ChangeStatus();
            timeChenge = false;
            //Debug.Log("時間停止が終了しました" + timeChenge);
        }

        time += Time.deltaTime;
        if (time > rate)
        {
            ShootClosestTarget();
            time = 0;
        }
    }

    // 距離が一番近い敵をターゲットに指定するメソッド
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

    // 弾を発射するメソッド
    void Shoot(GameObject targetObj)
    {
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
        ShotPattern(shooterPos, direction);

        //弾のサウンドを再生
        shotSound.Play();

        Debug.Log($"最も近いターゲット {targetObj.name} に弾を発射しました");
    }

    // 各キャラクターのステータス更新用メソッド
    public void ChangeStatus()
    {
        if (bulletPrefab == null)
        {
            Debug.Log("null");
            return;
        }
        // 主人公
        else if (bulletPrefab.name.Equals("HeroBullet") || bulletPrefab.name.Equals("Spark"))
        {
            bulletScripts.damage = PlayerStats.Instance.GetPlayerDamage();
            rate = PlayerStats.Instance.GetPlayerRate(defaultRate);
        }
        // 仲間１
        else if (bulletPrefab.name.Equals("SupporterBullet1"))
        {
            bulletScripts.damage = PlayerStats.Instance.GetSupporterDamage(1);
            rate = PlayerStats.Instance.GetSupporterRate(1, defaultRate);
            int supCount1 = PlayerStats.Instance.GetWeaponCount("Supporter1");
            if (supCount1 >= 1 && supLevel1 >= 0)
            {
                if (supCount1 >= 3 && supLevel1 >= 1)
                {
                    if (supCount1 >= 7 && supLevel1 >= 2)
                    {
                        supLevel1 = 3;
                        Debug.Log("仲間1のレベルが上がった：" + supLevel1);
                        return;
                    }
                    supLevel1 = 2;
                    Debug.Log("仲間1のレベルが上がった：" + supLevel1);
                    return;
                }
                supLevel1 = 1;
                Debug.Log("仲間1のレベルが上がった：" + supLevel1);
            }
        }
        // 仲間２
        else if (bulletPrefab.name.Equals("SupporterBullet2"))
        {
            bulletScripts.damage = PlayerStats.Instance.GetSupporterDamage(2);
            rate = PlayerStats.Instance.GetSupporterRate(2, defaultRate);
            int supCount2 = PlayerStats.Instance.GetWeaponCount("Supporter2");
            if (supCount2 >= 1 && supLevel2 >= 0)
            {
                if (supCount2 >= 3 && supLevel2 >= 1)
                {
                    if (supCount2 >= 7 && supLevel2 >= 2)
                    {
                        supLevel2 = 3;
                        Debug.Log("仲間2のレベルが上がった：" + supLevel2);
                        return;
                    }
                    supLevel2 = 2;
                    Debug.Log("仲間2のレベルが上がった：" + supLevel2);
                    return;
                }
                supLevel2 = 1;
                Debug.Log("仲間2のレベルが上がった：" + supLevel2);
            }
        }
        // 仲間３
        else if (bulletPrefab.name.Equals("SupporterBullet3"))
        {
            bulletScripts.damage = PlayerStats.Instance.GetSupporterDamage(3);
            rate = PlayerStats.Instance.GetSupporterRate(3, defaultRate);
            int supCount3 = PlayerStats.Instance.GetWeaponCount("Supporter3");
            if (supCount3 >= 1 && supLevel3 >= 0)
            {
                if (supCount3 >= 3 && supLevel3 >= 1)
                {
                    if (supCount3 >= 7 && supLevel3 >= 2)
                    {
                        supLevel3 = 3;
                        Debug.Log("仲間3のレベルが上がった：" + supLevel3);
                        return;
                    }
                    supLevel3 = 2;
                    Debug.Log("仲間3のレベルが上がった：" + supLevel3);
                    return;
                }
                supLevel3 = 1;
                Debug.Log("仲間3のレベルが上がった：" + supLevel3);
            }
        }
    }

    // 弾を発射するときのパターン
    public void ShotPattern(Vector2 shooterPos, Vector2 direction)
    {
        if (bulletPrefab == null)
        {
            Debug.Log("null");
            return;
        }
        else if (bulletPrefab.name.Equals("HeroBullet") || bulletPrefab.name.Equals("Spark")) 
        {
            BulletSpawn(shooterPos, direction);
        }
        else if (bulletPrefab.name.Equals("SupporterBullet1") && supLevel1 >= 2)
        {
            if (supLevel1 >= 3)
            {
                // 連射
                StartCoroutine(CallMethodRepeatedly(shooterPos, direction, 5));

            }
            else
            {
                // 連射
                StartCoroutine(CallMethodRepeatedly(shooterPos, direction, 3));
            }
        }
        else if (bulletPrefab.name.Equals("SupporterBullet2") && supLevel2 >= 2)
        {
            if (supLevel2 >= 3)
            {
                // 散弾
                BulletSpawn(shooterPos, direction);
                // 上の弾
                Vector2 upDirection = RotateVector(direction, angleOffset);
                BulletSpawn(shooterPos, upDirection);
                upDirection = RotateVector(direction, angleOffset * 2);
                BulletSpawn(shooterPos, upDirection);

                // 下の弾
                Vector2 downDirection = RotateVector(direction, -angleOffset);
                BulletSpawn(shooterPos, downDirection);
                downDirection = RotateVector(direction, -angleOffset * 2);
                BulletSpawn(shooterPos, downDirection);
            }
            else
            {
                // 散弾
                BulletSpawn(shooterPos, direction);
                // 上の弾
                Vector2 upDirection = RotateVector(direction, angleOffset);
                BulletSpawn(shooterPos, upDirection);

                // 下の弾
                Vector2 downDirection = RotateVector(direction, -angleOffset);
                BulletSpawn(shooterPos, downDirection);
            }
        }
        else if (bulletPrefab.name.Equals("SupporterBullet3") && supLevel3 >= 2)
        {
            // 貫通
            bulletPrefab.GetComponent<BulletController>().IsTrigger();
            bulletSpeed = 30f;
            if (supLevel3 >= 3)
            {
                // 弾のサイズを拡大
                BulletSpawn(shooterPos, direction);
            }
            else
            {
                BulletSpawn(shooterPos, direction);
            }
        }
        else
        {
            BulletSpawn(shooterPos, direction);
        }
    }

    // 弾を生成して動きをつけるメソッド
    public void BulletSpawn(Vector2 shooterPos, Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, shooterPos, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = direction * bulletSpeed;
        bullet.transform.Rotate(0f, 0f, 90f);
        if (supLevel3 >= 7)
        {
            bullet.transform.localScale = new Vector3(3f, 3f, 1f);
        }
    }

    // ベクトルを角度で回転させる関数
    private Vector2 RotateVector(Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }

    // 弾を連射して発射するときのメソッド
    IEnumerator CallMethodRepeatedly(Vector2 shooterPos, Vector2 direction, int x)
    {
        for (int i = 0; i < x; i++)
        {
            BulletSpawn(shooterPos, direction);
            yield return new WaitForSeconds(0.1f);
        }
    }


}
