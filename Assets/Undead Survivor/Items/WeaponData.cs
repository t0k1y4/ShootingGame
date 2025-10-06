using System;
using UnityEngine;

// 右クリックメニューから作成できるようにする
[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Item/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float attackPower;
    public Sprite icon;
    public int price;
    public String text;
    // 必要に応じて、武器の種類（ソード、アックスなど）、説明文などを追加
}