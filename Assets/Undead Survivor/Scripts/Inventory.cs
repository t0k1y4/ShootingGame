using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // インベントリUIのアイコンImageの配列
    [SerializeField] private Image[] itemIcons; // アイコン画像を表示するUIの配列
    [SerializeField] private TextMeshProUGUI[] quantityTexts;
    [SerializeField] private WeaponData[] allWeaponData;

    void OnEnable()
    {
        // PlayerStatsの武器変更イベントを購読し、UIUpdaterを呼び出す
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.OnWeaponsChanged += UIUpdater;
        }

        // 初回のUI更新
        UIUpdater();
    }

    // このGameObjectが無効になったときに呼ばれる
    void OnDisable()
    {
        // PlayerStatsの武器変更イベントの購読を解除する
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.OnWeaponsChanged -= UIUpdater;
        }
    }

    public void UIUpdater()
    {
        // 所持している武器のリスト
        List<WeaponData> ownedWeapons = new List<WeaponData>(PlayerStats.Instance.weapons.Keys);

        // 所持している武器を、allWeaponDataのインデックスに基づいて対応するスロットに表示する
        foreach (var ownedWeaponPair in ownedWeapons)
        {
            WeaponData weapon = ownedWeaponPair; // KeyValuePairからキー(WeaponData)を取得

            // allWeaponData配列内で、所持している武器が何番目にあるかを検索
            int index = System.Array.IndexOf(allWeaponData, weapon);
            String text = "" + PlayerStats.Instance.GetWeaponInt(weapon);

            // インデックスが有効な範囲内か確認
            if (index >= 0 && index < itemIcons.Length)
            {
                // 対応するUIスロットにアイコンを設定
                itemIcons[index].sprite = weapon.icon;
                quantityTexts[index].text = text;
            }
        }
    }
}