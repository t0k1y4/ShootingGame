using DG.Tweening;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    // Unityエディタから設定できるようにpublicな変数にする
    [SerializeField] private GameObject shopUI;

    // ショップUIの現在の表示状態
    private bool isShopOpen = false;
    // Tweenの重複実行を防止するためのフラグ
    private bool isAnimating = false;

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
        // アニメーション中は操作を受け付けない
        if (isAnimating) return;

        // ユーザーが 'B' キーを押したとき
        if (Input.GetKeyDown(KeyCode.B))
        {
            Shoping();
        }
    }

    public void Shoping()
    {
        isAnimating = true; // アニメーション開始

        if (isShopOpen)
        {
            // ショップを閉じる
            CloseShopUI();
        }
        else
        {
            // ショップを開く
            OpenShopUI();
        }

        isShopOpen = !isShopOpen;
    }

    private void OpenShopUI()
    {
        // ショップUIをアクティブにする
        shopUI.SetActive(true);
        Time.timeScale = 0f; // アニメーション開始前にゲームを一時停止

        // Tweenのシーケンスを作成
        Sequence sequence = DOTween.Sequence();
        
        // 最初の状態をセット（奥にあるように小さく設定）
        shopUI.transform.localScale = Vector3.zero;

        // 1. 一気に大きくするアニメーション
        sequence.Append(shopUI.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutQuad));

        // 2. 少し縮小して元のサイズに戻すアニメーション
        sequence.Append(shopUI.transform.DOScale(1f, 0.15f).SetEase(Ease.OutQuad));

        // シーケンスをTimeScaleの影響を受けないようにする
        sequence.SetUpdate(true);

        // シーケンス完了後に実行する処理
        sequence.OnComplete(() =>
        {
            isAnimating = false;
        });
    }

    private void CloseShopUI()
    {
        // Tweenのシーケンスを作成
        Sequence sequence = DOTween.Sequence();
        
        // 1. 少し縮小するアニメーション
        sequence.Append(shopUI.transform.DOScale(1.2f, 0.15f).SetEase(Ease.OutQuad));

        // 2. 奥に引っ込むように一気に小さくするアニメーション
        sequence.Append(shopUI.transform.DOScale(0f, 0.3f).SetEase(Ease.InQuad));

        // シーケンスをTimeScaleの影響を受けないようにする
        sequence.SetUpdate(true);

        // シーケンス完了後に実行する処理
        sequence.OnComplete(() =>
        {
            Time.timeScale = 1f; // アニメーション完了後にゲームを再開
            shopUI.SetActive(false);
            isAnimating = false;
        });
    }
}

