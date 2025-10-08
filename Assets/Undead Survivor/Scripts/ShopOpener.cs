using DG.Tweening;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    // Unityエディタから設定できるようにpublicな変数にする
    [SerializeField] private GameObject shopUI;
    // ショップUIのルートオブジェクトにCanvasGroupコンポーネントを追加してください
    [SerializeField] private CanvasGroup shopCanvasGroup;

    // ショップUIの現在の表示状態
    private bool isShopOpen = false;
    // Tweenの重複実行を防止するためのフラグ
    private bool isAnimating = false;

    void Start()
    {
        // ゲーム開始時はショップUIを非表示にしておく
        if (shopUI != null)
        {
            // CanvasGroupのalphaを0に設定
            shopCanvasGroup.alpha = 0f;
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

        // Tweenのシーケンスを作成
        Sequence sequence = DOTween.Sequence();
        
        // 最初にUIを少し縮小させるアニメーション
        shopUI.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        sequence.Append(shopUI.transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutBack));

        // 次に、元のスケールに戻すアニメーション
        sequence.Append(shopUI.transform.DOScale(1f, 0.15f).SetEase(Ease.OutQuad));

        // CanvasGroupのalphaを0から1に変化させる（フェードイン）アニメーション
        // 同時に動かしたいのでJoin()を使う
        sequence.Join(shopCanvasGroup.DOFade(1f, 0.3f));

        // シーケンス完了後に実行する処理
        sequence.OnComplete(() =>
        {
            Time.timeScale = 0f; // アニメーション完了後にゲームを一時停止
            isAnimating = false;
        });
    }

    private void CloseShopUI()
    {
        // Tweenのシーケンスを作成
        Sequence sequence = DOTween.Sequence();
        
        // UIを少し縮小させるアニメーション
        sequence.Append(shopUI.transform.DOScale(0.8f, 0.2f).SetEase(Ease.InBack));

        // CanvasGroupのalphaを1から0に変化させる（フェードアウト）アニメーション
        // 同時に動かしたいのでJoin()を使う
        sequence.Join(shopCanvasGroup.DOFade(0f, 0.2f));

        // シーケンス完了後に実行する処理
        sequence.OnComplete(() =>
        {
            Time.timeScale = 1f; // アニメーション完了後にゲームを再開
            shopUI.SetActive(false);
            isAnimating = false;
        });
    }
}
