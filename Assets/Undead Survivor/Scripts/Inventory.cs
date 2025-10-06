using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 武器を格納するリスト
    public List<WeaponData> weapons = new List<WeaponData>();

    // 武器を追加するメソッド
    public void AddWeapon(WeaponData newWeapon)
    {
        if (newWeapon != null)
        {
            weapons.Add(newWeapon);
            Debug.Log(newWeapon.weaponName + "をインベントリに追加しました。");
        }
    }
}