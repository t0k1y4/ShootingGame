using UnityEngine;
using UnityEngine.UI; // UIコンポーネントを使うために必要
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Aseprite;

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
                itemNames[i].text = smg.availableWeapons[i].weaponName;
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
    }

    public void RefleshShop()
    {
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
        if (PlayerStats.Instance.money < smg.availableWeapons[index].price) return;
        smg.BuyWeapon(index);
        buttons[index].interactable = false;
        money.text = PlayerStats.Instance.money + "$";
    }


}
