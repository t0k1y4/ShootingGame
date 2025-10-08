using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // インベントリUIのアイコンImageの配列
    [SerializeField] private Image[] itemIcons;

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
        // Dictionaryのキー（ユニークなWeaponData）をリストに変換
        List<WeaponData> uniqueWeapons = new List<WeaponData>(PlayerStats.Instance.weapons.Keys);

        // UIスロットを更新
        for (int i = 0; i < itemIcons.Length; i++)
        {
            if (i < uniqueWeapons.Count)
            {
                // UIに表示する武器のデータ
                WeaponData weapon = uniqueWeapons[i];

                itemIcons[i].sprite = weapon.icon; // アイコン画像を設定
                itemIcons[i].enabled = true; // 画像を表示
            }
            else
            {
                // アイテムがないスロットは非表示に
                itemIcons[i].sprite = null;
                itemIcons[i].enabled = false;
            }
        }
    }
}
