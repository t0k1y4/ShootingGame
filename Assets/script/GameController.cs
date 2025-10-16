using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class GameController : MonoBehaviour
{

    float coolTime;
    float killed = 0;       // 敵を倒した数
    int difficalty = 0;     // 難易度
    public int diff = 10;   // 難易度の上昇量

    public EnemyGenerator eg;
    public TextMeshProUGUI killedText;
    public TextMeshProUGUI difficaltyText;
    public TextMeshProUGUI wallHpText;
    public TextMeshProUGUI newText;
    public ShopManager shopManager;
    public ShopOpener shopOpener;
    public GameObject attackHitboxPrefab;
    public Image uAttackImg;
    public WeaponData userAttack;
    public float attackWidth = 4f;
    public float attackHeight = 2f;

    void OnEnable()
    {
        PlayerStats.Instance.ResetData();
    }
    void OnDisable()
    {
        // PlayerStatsの武器変更イベントの購読を解除する
        if (Wall.Instance != null)
        {
            Wall.Instance.OnWallHpChanged -= UIUpdater;
        }
    }

    void Start()
    {
        // WallHpChangedイベントを購読し、UIUpdaterを呼び出す
        if (Wall.Instance != null)
        {
            Wall.Instance.OnWallHpChanged += UIUpdater;
        }
        // 初回のUI更新
        UIUpdater();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && PlayerStats.Instance.UserIsAttack() == false&&Time.timeScale>0)
        {
            // マウスのスクリーン座標をワールド座標に変換
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(
                new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.transform.position.z * -1)
            );

            // プレイヤーの位置とクリック位置のVector2を取得
            Vector2 playerPosition2D = new Vector2(transform.position.x, transform.position.y);
            Vector2 clickPosition2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

            // プレイヤーの位置を中心とするRectを作成
            Rect attackRect = new Rect(
                playerPosition2D.x - attackWidth / 2,
                playerPosition2D.y - attackHeight / 2,
                attackWidth,
                attackHeight
            );

            // クリック位置が長方形内にあるか判定
            if (attackRect.Contains(clickPosition2D))
            {
                // 範囲内であれば攻撃判定を生成
                Instantiate(attackHitboxPrefab, new Vector3(clickPosition2D.x, clickPosition2D.y, 0), Quaternion.identity);
                StartCoroutine(CoolTime());
            }
            else
            {
                Debug.Log("攻撃範囲外です。");
            }
        }
    }

    public void KilledCount()
    {
        // 敵を倒したときにスコアを加算
        killed += 1;
        killedText.text = killed + ":killed !";
        // ハイスコア更新処理
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (killed > highScore)
        {
            //ハイスコアをセットする
            PlayerPrefs.SetInt("HighScore", (int)killed);
            //ハイスコアのセーブ
            PlayerPrefs.Save();
            Debug.Log("ハイスコア更新！: " + killed);
        }
        Difficalty();
    }

    public void Difficalty()
    {
        // スコアがdiffの倍数になったときに難易度が上昇
        if (killed % diff == 0)
        {
            difficalty += 1;
            difficaltyText.text = "difficalty:" + difficalty;
            eg.ChangeGenTime(difficalty);
            if (difficalty % 2 == 0)
            {
                //難易度が2の倍数になったらショップを開く
                BonusShop();
            }
        }

    }

    public void BonusShop()
    {
        PlayerStats.Instance.AddMoney(difficalty * 10);//数字は適当
        shopManager.RefreshShopItems();
        if (PlayerStats.Instance.IsAutoShop())
        {
            shopOpener.Shoping();
        }
        else newText.text = "NEW!!";
    }
    


    public void UIUpdater()
    {
        wallHpText.text = "HP : " + Wall.Instance.WallHp;
    }

    IEnumerator CoolTime()
    {
        coolTime = 1.6f - (PlayerStats.Instance.GetWeaponInt(userAttack) / 10);
        PlayerStats.Instance.UserAttack();
        uAttackImg.DOFillAmount(1, coolTime)
    .From(0) // 0から開始
    .SetEase(Ease.Linear);
        yield return new WaitForSeconds(coolTime);
        PlayerStats.Instance.UserAttackEnd();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // プレイヤーの位置を中心として長方形を描画
        Gizmos.DrawWireCube(transform.position, new Vector3(attackWidth, attackHeight, 0));
    }

}
