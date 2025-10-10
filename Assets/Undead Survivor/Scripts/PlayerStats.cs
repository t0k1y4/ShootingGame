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

    public float diameterRate = 0.05f;
    public float diameterSupRate = 0.03f;

    // 仲間のアクティブ状態
    bool supporter1 = false;
    bool supporter2 = false;
    bool supporter3 = false;


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

            // 仲間をアクティブに変更
            if (newWeapon.name.Contains("Supporter1"))
            {
                supporter1 = true;
                Debug.Log("仲間1がアクティブになりました");
            }
            else if (newWeapon.name.Contains("Supporter2"))
            {
                supporter2 = true;
                Debug.Log("仲間2がアクティブになりました");
            }
            else if (newWeapon.name.Contains("Supporter3"))
            {
                supporter3 = true;
                Debug.Log("仲間3がアクティブになりました");
            }
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
        supporter1 = false;
        supporter2 = false;
        supporter3 = false;
        foreach (var weapon in initialWeapons)
        {
            AddWeapon(weapon);
        }
        OnMoneyChanged?.Invoke();
        Debug.Log("PlayerStatsをリセットしました。");
    }

    // 仲間のアクティブ状態を取得
    public bool GetSupporter(int number)
    {
        switch (number)
        {
            case 1:
                return supporter1;

            case 2:
                return supporter2;

            case 3:
                return supporter3;

            default:
                return false;
        }

    }

    // 主人公のダメージの計算・取得
    public float GetPlayerDamage()
    {
        float totalDamage = 5;
        foreach (KeyValuePair<WeaponData, int> weapon in weapons)
        {
            if (weapon.Key.name.Contains("DamageUp"))
            {
                Debug.Log("主人公武器ダメージ計算");
                totalDamage += GetWeaponPower(weapon.Key);
            }
        }
        Debug.Log("現在のダメージ：" + totalDamage);
        return totalDamage;
    }

    public float GetPlayerRate(float defaultRate)
    {
        float rate = 0;
        foreach (KeyValuePair<WeaponData, int> weapon in weapons)
        {
            if (weapon.Key.name.Contains("RateUp"))
            {
                Debug.Log("主人公発射レート計算");
                rate = defaultRate / (diameterRate * GetWeaponRate(weapon.Key) + 1f);
            }
        }
        Debug.Log("現在のレート：" + rate);
        return rate;
    }

    // 仲間のダメージの計算・取得
    public float GetSupporterDamage(int number)
    {
        float totalDamage = 5;
        switch (number)
        {
            case 1:
                foreach (KeyValuePair<WeaponData, int> weapon in weapons)
                {
                    if (weapon.Key.name.Contains("Supporter1"))
                    {
                        Debug.Log("１武器ダメージ計算");
                        totalDamage += GetWeaponPower(weapon.Key);
                    }
                }
                return totalDamage;

            case 2:
                foreach (KeyValuePair<WeaponData, int> weapon in weapons)
                {
                    if (weapon.Key.name.Contains("Supporter2"))
                    {
                        Debug.Log("２武器ダメージ計算");
                        totalDamage += GetWeaponPower(weapon.Key);
                    }
                }
                return totalDamage;

            case 3:
                foreach (KeyValuePair<WeaponData, int> weapon in weapons)
                {
                    if (weapon.Key.name.Contains("Supporter3"))
                    {
                        Debug.Log("３武器ダメージ計算");
                        totalDamage += GetWeaponPower(weapon.Key);
                    }
                }
                return totalDamage;

            default:
                return 0;
        }
    }
    public float GetSupporterRate(int number, float defaultRate)
    {
        float rate = 1f;
        switch (number)
        {
            case 1:
                foreach (KeyValuePair<WeaponData, int> weapon in weapons)
                {
                    if (weapon.Key.name.Contains("Supporter1"))
                    {
                        Debug.Log("１発射レート計算");
                        rate = defaultRate / (diameterRate * GetWeaponRate(weapon.Key) + 1f);
                    }
                }
                return rate;

            case 2:
                foreach (KeyValuePair<WeaponData, int> weapon in weapons)
                {
                    if (weapon.Key.name.Contains("Supporter2"))
                    {
                        Debug.Log("２発射レート計算");
                        rate = defaultRate / (diameterRate * GetWeaponRate(weapon.Key) + 1f);
                    }
                }
                return rate;

            case 3:
                foreach (KeyValuePair<WeaponData, int> weapon in weapons)
                {
                    if (weapon.Key.name.Contains("Supporter3"))
                    {
                        Debug.Log("３発射レート計算");
                        rate = defaultRate / (diameterRate * GetWeaponRate(weapon.Key) + 1f);
                    }
                }
                return rate;

            default:
                return 0;
        }
    }

    

}
