using UnityEngine;

public class EnemyBomber : MonoBehaviour
{
    public float hp = 10.0f;
    public float difficalty = 1.0f;
    public float pow = 1.0f;
    public float point = 1.0f;
    public float speed = 1.0f;
    public bool isBound;
    public float bombTimer = 5f;
    Wall wallInstance;
    Animator at;
    Rigidbody2D rb;



    void Start()
    {
        hp *= difficalty;
        pow *= difficalty;
        at = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
        wallInstance = Wall.Instance;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        // velocityを直接操作して、X軸方向の速度を一定に保つ
        rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
    }

    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 壁にぶつかったら速度を0に
            rb.linearVelocity = Vector2.zero;
            //GetComponent<Animator>().SetBool("a", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            {
                Damage(collision.gameObject.GetComponent<BulletController>().damage);
            }
        }
    }


    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Wall"))
        {
            bombTimer -= Time.deltaTime;
            Debug.Log("爆発まで : " + bombTimer);
            if (bombTimer < 0)
            {
                wallInstance.WallDamage(pow);
                Destroy(gameObject);
            }
        }
    }
}
