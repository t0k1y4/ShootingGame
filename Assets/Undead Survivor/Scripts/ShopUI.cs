using UnityEngine;
using UnityEngine.UI; // UIコンポーネントを使うために必要
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEditor.PackageManager.Requests;

public class ShopUI : MonoBehaviour
{
    // ショップに並ぶ武器のデータ（ShopManagerから取得）
    [SerializeField] private ShopManager smg;

    // 各アイテムスロットのテキストコンポーネントを格納する配列
    // Inspectorで直接紐づけする
    [SerializeField] private TextMeshProUGUI[] itemNames;
    [SerializeField] private Image[] itemIcons;
    [SerializeField] private Button[] buttons;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private TextMeshProUGUI refPrice;

    AudioSource equipment; //ストップボタンのオーディオソース
    public AudioClip equipSound;  //装備品を購入したときの音
    public AudioClip reloadSound;  //リロードボタンを押したときの音

    private Color defaultColor = Color.yellow;

    void Awake()
    {
        equipment = GetComponent<AudioSource>();
        
    }


    [SerializeField] private int refleshPrice=5;
    void OnEnable()
    {
        money.text = PlayerStats.Instance.money + "$";
        message.text = "What do you want?";
    }

    // ショップUIの更新メソッド
    public void UpdateShopUI()
    {
        // 念のため、子要素の数とアイテムの数が一致するか確認
        if (itemNames.Length != smg.availableWeapons.Count)
        {
            Debug.LogWarning("アイテムスロットの数とアイテムデータの数が一致しません。");
        }

        // availableWeaponsのデータを使って、子要素のUIを更新する
        for (int i = 0; i < itemNames.Length; i++)
        {
            // availableWeaponsのインデックスが配列の範囲内かチェック
            if (i < smg.availableWeapons.Count)
            {
                itemNames[i].text = smg.availableWeapons[i].weaponName + "\n$" +
                                    smg.availableWeapons[i].price;
                itemNames[i].color = defaultColor;
                itemIcons[i].sprite = smg.availableWeapons[i].icon;
                buttons[i].interactable = true;
            }
            else
            {
                // データがないスロットは非表示にするなどの処理
                itemNames[i].text = "";
                itemIcons[i].sprite = null;
                buttons[i].interactable = false;
            }
        }
        money.text = PlayerStats.Instance.money + "$";
        refPrice.text = "Reload" + "\n$" + refleshPrice;
    }

    public void RefleshShop()
    {
        if (PlayerStats.Instance.money < refleshPrice) return;
        //更新ボタンの音を鳴らす
        equipment.clip = reloadSound;
        equipment.Play();
        PlayerStats.Instance.money -= refleshPrice;
        refleshPrice +=(int)refleshPrice/2;
        smg.RefreshShopItems();
        UpdateShopUI();
    }

    public void BonusShop()
    {
        refleshPrice = 5;
        smg.RefreshShopItems();
        UpdateShopUI();
    }

    public void Button0()
    {
        Buy(0);
    }

    public void Button1()
    {
        Buy(1);
    }

    public void Button2()
    {
        Buy(2);
    }
    public void Button3()
    {
        Buy(3);
    }

    void Buy(int index)
    {
        // ① 所持金が足りなければ購入処理を中断
        if (PlayerStats.Instance.money < smg.availableWeapons[index].price) return;
        // ② 武器購入処理
        smg.BuyWeapon(index);
        //武器装備音を鳴らす
        equipment.clip = equipSound;
        equipment.Play();
        // ③ 該当ボタンを非アクティブに（再購入防止）
        buttons[index].interactable = false;
        // ④ アイテム名の色を黒に変更（購入済みの視覚的表現）
        itemNames[index].color = Color.black;
        // ⑤ 所持金表示を更新
        money.text = "$" + PlayerStats.Instance.money;
    }


}
