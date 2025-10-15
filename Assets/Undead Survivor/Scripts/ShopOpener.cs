using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    // Unityエディタから設定できるようにpublicな変数にする
    [SerializeField] private GameObject shopUI;
    [SerializeField] private RectTransform traderUI;
    [SerializeField] private GameObject messageUI;
    [SerializeField] private TextMeshProUGUI messageUIText;
    [SerializeField] private TextMeshProUGUI autoshop;

    public Vector2 traderTargetPosition;// 店員が登場する画面内の最終位置
    public Vector2 traderInitialPosition;// 店員が表示される初期位置（画面外）
    AudioSource shopSound; //オーディオソース
    public AudioClip openSound; //ショップが開くときの音
    public AudioClip closeSound; //ショップが閉じるときの音

    // ショップUIの現在の表示状態
    private bool isShopOpen = false;
    // Tweenの重複実行を防止するためのフラグ
    private bool isAnimating = false;


    void Start()
    {
        shopSound = GetComponent<AudioSource>();
        // ゲーム開始時はショップUIを非表示にしておく
        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }
        if (traderUI != null)
        {
            traderUI.gameObject.SetActive(false);
            traderUI.anchoredPosition = traderInitialPosition; // 初期位置を設定
        }
        if (messageUI != null)
        {
            messageUI.SetActive(false);
        }
    }

    void Update()
    {

    }

    public void Shoping()
    {
        // アニメーション中は操作を受け付けない
        if (isAnimating) return;
        isAnimating = true; // アニメーション開始

        if (isShopOpen)
        {
            // ショップを閉じる
            CloseShopUI();
            //サウンド再生
            shopSound.clip = closeSound;
            shopSound.Play();
        }
        else
        {
            // ショップを開く
            OpenShopUI();
            //サウンド再生
            shopSound.clip = openSound;
            shopSound.Play();
        }

        isShopOpen = !isShopOpen;
    }

    private void OpenShopUI()
    {
        // まず店員を画面外から登場させるシーケンス
        Sequence traderSequence = DOTween.Sequence();


        traderUI.gameObject.SetActive(true); // 店員UIをアクティブにする
        traderUI.anchoredPosition = traderInitialPosition; // 念のため初期位置にセット

        traderSequence.Append(traderUI.DOAnchorPos(traderTargetPosition, 0.5f).SetEase(Ease.OutBack));

        // 店員の登場が終わった後に、ショップUIを開くアニメーションを実行する
        traderSequence.OnComplete(() =>
        {
            // ショップUIをアクティブにする
            shopUI.SetActive(true);
            messageUI.SetActive(true);
            messageUIText.text = "What Do You Want?";
            Time.timeScale = 0f; // アニメーション開始前にゲームを一時停止

            // Tweenのシーケンスを作成
            Sequence shopSequence = DOTween.Sequence();

            // 最初の状態をセット（奥にあるように小さく設定）
            shopUI.transform.localScale = Vector3.zero;
            messageUI.transform.localScale = Vector3.zero;

            // 1. 一気に大きくするアニメーション
            shopSequence.Append(shopUI.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutQuad));
            shopSequence.Join(messageUI.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutQuad));

            // 2. 少し縮小して元のサイズに戻すアニメーション
            shopSequence.Append(shopUI.transform.DOScale(1f, 0.15f).SetEase(Ease.OutQuad));
            shopSequence.Join(messageUI.transform.DOScale(1f, 0.15f).SetEase(Ease.OutQuad));

            // シーケンスをTimeScaleの影響を受けないようにする
            shopSequence.SetUpdate(true);

            // シーケンス完了後に実行する処理
            shopSequence.OnComplete(() =>
            {
                isAnimating = false;
            });
        });
    }

    private void CloseShopUI()
    {
        // Tweenのシーケンスを作成
        Sequence sequence = DOTween.Sequence();
        messageUIText.text = "bye~~~";

        // 1. 少し縮小するアニメーション
        sequence.Append(shopUI.transform.DOScale(1.2f, 0.15f).SetEase(Ease.OutQuad));

        // 2. 奥に引っ込むように一気に小さくするアニメーション
        sequence.Append(shopUI.transform.DOScale(0f, 0.3f).SetEase(Ease.InQuad));
        sequence.Append(messageUI.transform.DOScale(1.2f, 0.15f).SetEase(Ease.OutQuad));
        sequence.Append(messageUI.transform.DOScale(0f, 0.3f).SetEase(Ease.InQuad));
        sequence.Join(traderUI.DOAnchorPos(traderInitialPosition, 0.5f).SetEase(Ease.InBack));

        // シーケンスをTimeScaleの影響を受けないようにする
        sequence.SetUpdate(true);

        // シーケンス完了後に実行する処理
        sequence.OnComplete(() =>
        {
            Time.timeScale = 1f; // アニメーション完了後にゲームを再開
            shopUI.SetActive(false);
            messageUI.SetActive(false);
            traderUI.gameObject.SetActive(false); // ショップUIが消えたら店員も非表示にする
            isAnimating = false;
        });
    }


    public void CheckButton()
    {
        PlayerStats.Instance.AutoShopButton();
        if (PlayerStats.Instance.IsAutoShop())
        {
            autoshop.text = "ON";
        }
        else autoshop.text = "OFF";
    }
}