using UnityEngine;

public class EnemyBomber : MonoBehaviour
{
    AudioSource deadSound;
    public float hp = 10.0f;
    public float difficalty = 1.0f;
    public float pow = 1.0f;
    public float point = 1.0f;
    public float speed = 1.0f;
    public bool isBound;
    public float atkInterval = 1.0f;
    public float atkTimer = 0f;
    float time = 0.1f;
    Wall wallInstance;
    Animator at;
    Rigidbody2D rb;
    GameObject gc;
    GameController gameController;
    public float bombTimer = 5f;

    void Start()
    {
        hp *= difficalty;
        pow *= difficalty;
        at = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
        wallInstance = Wall.Instance;
        gc = GameObject.Find("GameController");
        gameController = gc.GetComponent<GameController>();

        //死んだ音
        deadSound = GetComponent<AudioSource>();
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
        //deadSound.Play();
        if (hp <= 0)
        {
            PlayerStats.Instance.AddMoney(1 * (int)difficalty);
            gameController.KilledCount();
            at.SetTrigger("Dead");
            float animationLength = at.GetCurrentAnimatorStateInfo(0).length;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            transform.localScale *= 2f;
            //敵が死んだらサウンドを再生
            AudioSource.PlayClipAtPoint(deadSound.clip, transform.position);
            Destroy(gameObject, animationLength);
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


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 壁にぶつかったら速度を0に
            rb.linearVelocity = Vector2.zero;
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
