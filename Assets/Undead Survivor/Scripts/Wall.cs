using UnityEngine;

public class Wall : MonoBehaviour
{
    public static Wall Instance { get; private set; }

    public float WallHp { get; private set; }
    public float WallMaxHp { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void WallDamage(float damage)
    {
        if (WallHp <= 0) return;
        Debug.Log(WallHp);
        WallHp -= damage;
        Debug.Log(WallHp);
        if (WallHp <= 0)
        {
            Debug.Log("ゲームオーバー");
            // ゲームオーバー処理をここに記述
            // GameManager.Instance.GameOver();
        }
    }

    //最大値を増やせる処理
    public void wallCustom(float maxUp)
    {
        WallMaxHp += maxUp;
        WallHp += maxUp;
    }

    //最大値を増やすときに乗算にできるオーバーロード
    public void wallCustom(float maxUp, bool multiplication)
    {
        if (multiplication)
        {
            WallMaxHp *= maxUp;
            WallHp *= maxUp;
        }
        else wallCustom(maxUp);
    }

    //壁を回復するだけの処理
    public void wallRecover(float heal)
    {
        WallHp += heal;
        WallHp = Mathf.Min(WallHp, WallMaxHp);
    }
}

