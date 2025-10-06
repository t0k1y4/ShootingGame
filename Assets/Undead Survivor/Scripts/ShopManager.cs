using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // プレイヤーのインベントリへの参照
    public Inventory playerInventory;

    // --- ここから追加 ---
    // 販売する可能性のある全武器データ
    [SerializeField] private List<WeaponData> allWeapons = new List<WeaponData>();

    // ショップに並ぶ武器のデータ
    [SerializeField] private List<WeaponData> availableWeapons = new List<WeaponData>();

    // ショップに並べる武器の数
    [SerializeField] private int numberOfItemsInShop = 4;

    // ショップUIのルートオブジェクト
    [SerializeField] private GameObject shopUI;

    // --- ここまで追加 ---

    private void Start()
    {
        // ゲーム開始時やショップの初回オープン時に売り物をランダムに設定
        RefreshShopItems();
    }

    // ショップの売り物をランダムに設定するメソッド
    public void RefreshShopItems()
    {
        // 売り物をリセット
        availableWeapons.Clear();

        // 全ての武器データが登録されているか確認
        if (allWeapons.Count == 0)
        {
            Debug.LogError("販売できる武器データがありません！");
            return;
        }

        // 重複しないようにランダムな武器を選ぶ
        List<WeaponData> tempWeapons = new List<WeaponData>(allWeapons);
        for (int i = 0; i < numberOfItemsInShop; i++)
        {
            if (tempWeapons.Count == 0)
            {
                break; // すべての武器が選択されたらループを抜ける
            }

            int randomIndex = Random.Range(0, tempWeapons.Count);
            availableWeapons.Add(tempWeapons[randomIndex]);
            tempWeapons.RemoveAt(randomIndex); // 重複を避ける
        }

        // ここでUIを更新する処理を呼び出す
        UpdateShopUI();
    }

    // UIを更新するメソッド（UIスクリプトに任せることも多い）
    private void UpdateShopUI()
    {
        // TODO: availableWeaponsのリストを使ってUIを更新する
        // 例: UIのボタンに武器のアイコンと名前を表示する
        Debug.Log("ショップの売り物を更新しました。");
        for(int i = 0; i < availableWeapons.Count; i++)
        {
            Debug.Log($"商品 {i+1}: {availableWeapons[i].weaponName}");
        }
    }

    // 購入ボタンのOnClickイベントで呼び出すメソッド
    // indexで何番目の武器を買ったかを指定する
    public void BuyWeapon(int index)
    {
        if (index >= 0 && index < availableWeapons.Count)
        {
            WeaponData weaponToBuy = availableWeapons[index];
            if (playerInventory != null && weaponToBuy != null)
            {
                playerInventory.AddWeapon(weaponToBuy);
            }
        }
    }
}