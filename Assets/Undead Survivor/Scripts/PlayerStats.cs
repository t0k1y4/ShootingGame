using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// 右クリックメニューから作成できるようにする
[CreateAssetMenu(fileName = "Player Stats", menuName = "Game Data/Player Stats")]
public class PlayerStats : SingletonScriptableObject<PlayerStats>
{
    // ユーザーの所持金
    public int money;
    public Dictionary<WeaponData, int> weapons = new Dictionary<WeaponData, int>();

    [SerializeField] private int initialMoney = 0;
    [SerializeField] private List<WeaponData> initialWeapons = new List<WeaponData>();

    public event Action OnWeaponsChanged;
    public event Action OnMoneyChanged;

    // 所持金を増やすメソッド
    public void AddMoney(int amount)
    {
        money += amount;
        OnMoneyChanged?.Invoke();
    }

    public void UsemMoney(int amount)
    {
        money -= amount;
        OnMoneyChanged?.Invoke();
    }


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
        OnWeaponsChanged?.Invoke();
    }

    public float GetWeaponPower(WeaponData weapon)
    {
        return weapon.attackPower * weapons[weapon];
    }

    public float GetWeaponRate(WeaponData weapon)
    {
        return weapon.rate * weapons[weapon];
    }

    public void ResetData()
    {
        money = initialMoney;
        weapons.Clear();
        foreach (var weapon in initialWeapons)
        {
            AddWeapon(weapon);
        }
        OnMoneyChanged?.Invoke();
        Debug.Log("PlayerStatsをリセットしました。");
    }
    


}
