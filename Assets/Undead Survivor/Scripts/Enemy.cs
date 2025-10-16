using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using DG.Tweening;
using System.Collections;

public class Enemy : MonoBehaviour
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
    public float damageInterval = 0.5f;
    Wall wallInstance;
    Animator at;
    Rigidbody2D rb;
    GameObject gc;
    BoxCollider2D bc;
    GameController gameController;
    public float bombTimer = 5f;
    bool isBomb;
    bool isDamaging;

    void Start()
    {
       
        at = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
        wallInstance = Wall.Instance;
        gc = GameObject.Find("GameController");
        gameController = gc.GetComponent<GameController>();
        difficalty = gameController.difficalty;
        hp = hp*((difficalty/3)+1);
        pow *= difficalty;
        if (gameObject.name.Contains("Bomb"))
        {
            isBomb = true;
        }
        else
        {
            isBomb = false;
        }

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
        at.SetTrigger("Hit");
        if (hp <= 0)
        {
            PlayerStats.Instance.AddMoney(1 * (int)difficalty);
            gameController.KilledCount();
            if (gameObject.name.Contains("Boss"))
            {
                at.SetBool("DeadBoss", true);
            }
            else
            {
                at.SetBool("Dead", true);
            }
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
                if (collision.gameObject.GetComponent<BulletController>() != null)
                {
                    Damage(collision.gameObject.GetComponent<BulletController>().damage);
                }
                else if (collision.gameObject.GetComponent<Spark>() != null && !isDamaging)
                {
                    Debug.Log("スパークダメージ");
                    StartCoroutine(DamageOverTime(collision.gameObject.GetComponent<Spark>().damage));
                }
                else
                {
                    return;
                }
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
        if (collision.gameObject.CompareTag("Wall") && !isBomb)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer > atkInterval)
            {
                wallInstance.WallDamage(pow);
                atkTimer = 0;
            }
        }
        else if (collision.gameObject.CompareTag("Wall") && isBomb)
        {
            bombTimer -= Time.deltaTime;
            Debug.Log("爆発まで : " + bombTimer);
            if (bombTimer < 0)
            {
                wallInstance.WallDamage(pow);
                bc.isTrigger = true;
                at.SetBool("Bomb", true);
                float animationLength = at.GetCurrentAnimatorStateInfo(0).length;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                transform.localScale *= 2f;
                AudioSource.PlayClipAtPoint(deadSound.clip, transform.position);
                Destroy(gameObject, animationLength);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Spark>() != null)
        {
            isDamaging = false;
        }
    }

    IEnumerator DamageOverTime(float damage)
    {
        isDamaging = true;
        while (isDamaging)
        {
            // ダメージ処理
            Damage(damage);

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
