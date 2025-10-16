using UnityEngine;

public class SparkShooter : MonoBehaviour
{
    AudioSource shotSound;          //打つ時の音
    public string targetTag = "Enemy";     // ターゲットのタグ名
    public GameObject bulletPrefab;         // 弾のPrefab
    public float bulletSpeed = 10f;         // 弾速（固定）
    public float time = 0;
    public float rate = 10f;
    public float angleRange = 35f; // 発射角度の範囲（例：±17.5度）
    AudioSource sparkSound;

    void Start()
    {
        sparkSound = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > rate && PlayerStats.Instance.IsSpark())
        {
            Debug.Log(PlayerStats.Instance.IsSpark());
            Shoot();
            time = 0;
        }
        else if (time > rate)
        {
            time = 0;
        }
    }

    public void Shoot()
    {
        // 音の再生
        sparkSound.Play();

        // 角度をランダムに決定（中心は180度、範囲は±angleRange/2）
        float randomAngle = Random.Range(180f - angleRange / 2f, 180f + angleRange / 2f);

        // ラジアンに変換
        float radian = randomAngle * Mathf.Deg2Rad;

        // 左方向（-1, 0）を基準に回転
        Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;

        // 弾を生成
        bulletPrefab = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // 弾に速度を与える
        Rigidbody2D rb = bulletPrefab.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;
    }

}
