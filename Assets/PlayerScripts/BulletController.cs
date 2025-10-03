using UnityEngine;

public class BulletController : MonoBehaviour
{
    public string targetTag = "Enemy";
    public float damage = 5f;
    public Enemy te;

    void OnCollisionEnter2D(Collision2D other)
    {
    if (other.gameObject.CompareTag("Enemy"))
        {
            te = other.collider.GetComponent<Enemy>();
            if(te != null){
            te.Damage(damage);
        }
        }
        Destroy(gameObject);
    }
}
