using UnityEngine;

public class BulletController : MonoBehaviour
{
    public string targetTag = "Enemy";
    public float damage = 5f;
    public Enemy enemy;

    void Update()
    {
        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log(damage + ":ダメージ与えた");
                enemy.Damage(damage);
            }
        }
        Destroy(gameObject);
    }

    public void IsTrigger()
    {
        gameObject.GetComponent<Collider2D>().isTrigger = true;
    }
    
}
