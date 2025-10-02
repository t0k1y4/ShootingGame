using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public float hp=10;
    public float difficalty=1;
    public float pow=1;


    // 回転の中心点
    Vector2 center;
    // 半径
    public float radius = 3f;
    // 角度
    private float angle = 0f;
    // 回転の速さ
    public float angularSpeed = 1f;

    void Start()
    {
        hp *= difficalty;
        pow *= difficalty;
        center = transform.position;
    }

    void Update()
    {
        angle += angularSpeed * Time.deltaTime;

        // X座標とY座標を計算
        float x = center.x + Mathf.Cos(angle) * radius;
        float y = center.y + Mathf.Sin(angle) * radius;

        // 位置を更新
        transform.position = new Vector2(x, y);

        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float damage)
    {
        hp -= damage;
    }
}
