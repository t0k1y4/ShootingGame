using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class GameController : MonoBehaviour
{

    float coolTime ;
    float killed = 0;       // 敵を倒した数
    int difficalty = 0;     // 難易度
    public int diff = 10;   // 難易度の上昇量

    public EnemyGenerator eg;
    public TextMeshProUGUI killedText;
    public TextMeshProUGUI difficaltyText;
    public TextMeshProUGUI wallHpText;
    public ShopManager shopManager;
    public ShopOpener shopOpener;
    public GameObject attackHitboxPrefab;
    public Image uAttackImg;
    public WeaponData userAttack;

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
        // 左マウスボタンがクリックされた瞬間を検出
        if (Input.GetMouseButtonDown(0) && PlayerStats.Instance.UserIsAttack() == false)
        {
            // マウスのスクリーン座標をワールド座標に変換
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // 2Dゲームなのでz座標は0に固定

            // 攻撃判定を生成
            Instantiate(attackHitboxPrefab, mousePosition, Quaternion.identity);
            StartCoroutine(CoolTime());
        }
    }

    public void KilledCount()
    {
        // 敵を倒したときにスコアを加算
        killed += 1;
        killedText.text = killed + ":killed !";
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
        shopOpener.Shoping();
    }

    public void UIUpdater()
    {
        wallHpText.text = "HP : " + Wall.Instance.WallHp;
    }

    IEnumerator CoolTime()
    {
        coolTime = 1.6f - (PlayerStats.Instance.GetWeaponInt(userAttack)/10);
        PlayerStats.Instance.UserAttack();
        uAttackImg.DOFillAmount(1, coolTime)
    .From(0) // 0から開始
    .SetEase(Ease.Linear);
        yield return new WaitForSeconds(coolTime);
        PlayerStats.Instance.UserAttackEnd();
    }


}
