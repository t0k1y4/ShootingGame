using UnityEngine;

public class BulletController : MonoBehaviour
{
    void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }
}
