using DG.Tweening;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    // Unityエディタから設定できるようにpublicな変数にする
    [SerializeField] private GameObject shopUI;

    // ショップUIの現在の表示状態
    private bool isShopOpen = false;

    void Start()
    {
        // ゲーム開始時はショップUIを非表示にしておく
        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }
    }

    void Update()
    {
        // ユーザーが 'B' キーを押したとき
        if (Input.GetKeyDown(KeyCode.B))//テスト用にBを押しても開くように
        {
            Shoping();
        }
    }

    public void Shoping()
    {
        isShopOpen = !isShopOpen;
        shopUI.SetActive(isShopOpen);
        if (isShopOpen)
        {
            Time.timeScale = 0f; // ゲームを一時停止
            shopUI.transform.DOScale(1.1f, 0.2f);
            shopUI.transform.DOScale(1.0f, 0.3f);
        }
        else
        {
            shopUI.transform.DOScale(1.1f, 0.3f);
            shopUI.transform.DOScale(1.0f, 0.2f);
            Time.timeScale = 1f; // ゲームを再開
        }
    }
}
