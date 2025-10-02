using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp=10.0f;
    public float difficalty=1.0f;
    public float pow=1.0f;
    public float point = 1.0f;
    public float speed = 1.0f;
    Animator at;
    Rigidbody2D rb;



    void Start()
    {
        hp *= difficalty;
        pow *= difficalty;
        at = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
    }

    void Update()
    {


        if (hp <= 0)
        {
            Destroy(gameObject);
        }

    
    }
    
    void FixedUpdate()
    {
        // velocityを直接操作して、X軸方向の速度を一定に保つ
        rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
    }

    public void Damage(float damage)
    {

        hp -= damage;
    }
}
