using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{

    private float damage; // 攻撃力
    public WeaponData weapon;

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
