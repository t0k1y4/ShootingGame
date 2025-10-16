using UnityEngine;
using System.Collections;

public class Spark : MonoBehaviour
{
    public float damage; // 攻撃力
    public WeaponData weapon;
    Collider2D enemy;
    public float damageInterval = 0.5f;
    private bool isDamaging = false;
    AudioSource sparkSound;

    void Awake()
    {
        sparkSound = gameObject.GetComponent<AudioSource>();
        sparkSound.Play();
        damage = PlayerStats.Instance.GetWeaponInt(weapon) * weapon.attackPower;
        Destroy(gameObject, 20f);
        InvokeRepeating("PlaySound", 0f, 2f);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isDamaging)
        {
            Debug.Log("スパークダメージ");
            StartCoroutine(DamageOverTime(other));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isDamaging = false;
        }
    }

    IEnumerator DamageOverTime(Collider2D other)
    {
        isDamaging = true;
        while (isDamaging && enemy != null)
        {
            // ダメージ処理
            other.GetComponent<Enemy>().Damage(damage);

            yield return new WaitForSeconds(damageInterval);
        }
    }

        void PlaySound()
    {
        Debug.Log("音を再生");
        sparkSound.Play();
    }


}
