using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // 武器の種類と個数を管理するDictionary
    public Dictionary<WeaponData, int> weapons = new Dictionary<WeaponData, int>();

    GameObject sup1;
    GameObject sup2;
    GameObject sup3;

    // インベントリUIのアイコンImageの配列
    [SerializeField] private Image[] itemIcons;
    // アイテムの個数を表示するテキストの配列
    //[SerializeField] private Text[] itemCounts;

    // 武器を追加するメソッド
    public void AddWeapon(WeaponData newWeapon)
    {
        if (newWeapon != null)
        {
            if (weapons.ContainsKey(newWeapon))
            {
                weapons[newWeapon]++; // すでに持っているなら個数を増やす
            }
            else
            {
                weapons.Add(newWeapon, 1); // 持っていなければ追加する
            }
            Debug.Log(newWeapon.weaponName + "をインベントリに追加しました。現在の個数: " + weapons[newWeapon]);
        }
        UIUpdater();

        if (newWeapon.weaponName == "Supporter1")
        {
            sup1 = GameObject.Find("Supporter1");
            sup1.SetActive(true);
        }
    }

    // 武器を削除するメソッド（必要に応じて実装）
    public void RemoveWeapon(WeaponData weaponToRemove)
    {
        if (weaponToRemove != null && weapons.ContainsKey(weaponToRemove))
        {
            weapons[weaponToRemove]--;
            if (weapons[weaponToRemove] <= 0)
            {
                weapons.Remove(weaponToRemove);
            }
        }
        UIUpdater();
    }

    void UIUpdater()
    {
        // Dictionaryのキー（ユニークなWeaponData）をリストに変換
        List<WeaponData> uniqueWeapons = new List<WeaponData>(weapons.Keys);

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
