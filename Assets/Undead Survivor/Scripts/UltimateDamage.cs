using System.Collections;
using UnityEngine;

public class UltimateDamage : MonoBehaviour
{

    private float damage; // 攻撃力
    public WeaponData weapon;
    Collider2D enemy;
    void Start()
    {
        damage = PlayerStats.Instance.GetWeaponInt(weapon) * weapon.attackPower;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 接触したオブジェクトにEnemyタグが付いているか確認
        if (other.CompareTag("Enemy"))
        {
        other.GetComponent<Enemy>().Damage(damage);
        Debug.Log(other.name + "に" + damage + "ダメージを与えました！");
        }
    }

    public void DestroySelf()
    {

        Destroy(gameObject);

    }
}

