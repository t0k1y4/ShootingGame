using UnityEngine;

public class BulletController : MonoBehaviour
{
    public string targetTag = "Enemy";
    public float damege = 5f;
    public TestEnemy te;

    void OnCollisionEnter2D(Collision2D other)
    {
    if (other.gameObject.CompareTag("Enemy"))
        {
            te = other.collider.GetComponent<TestEnemy>();
            if(te != null){
            te.Damage(damege);
        }
        }
        Destroy(gameObject);
    }
}
