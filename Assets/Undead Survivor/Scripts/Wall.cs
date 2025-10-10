using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Wall : MonoBehaviour
{
    public GameObject GameOverCanvas; //gameover表示オブジェクト
    public GameObject IsRetryCanvas; //continue?表示オブジェクト

    public static Wall Instance { get; private set; }
    AudioSource destroySound;    //バリケード崩壊音
    public float WallHp { get; private set; }
    public float WallMaxHp { get; private set; }
    public event Action OnWallHpChanged;


    public void OnRetryYes() //リトライする
    {
        Time.timeScale = 1f; // ポーズ解除
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 現在のシーンを再読み込み
    }

    public void OnRetryNo() //リトライしない
    {
        Time.timeScale = 1f; // ポーズ解除
        SceneManager.LoadScene("StartScene"); // タイトル画面など別シーン名に変更
    }


    private void Awake()
    {
        //gameoverは最初非表示
        //IsRetryCanvas.SetActive(false);
        //  GameOverCanvas.SetActive(false);

        //死んだ音
        destroySound = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            WallMaxHp = 100;
            WallHp = WallMaxHp;
            Debug.Log(WallHp);
            
        }
        else
        {
            Destroy(gameObject);
        }
        OnWallHpChanged?.Invoke();
    }

    public void WallDamage(float damage)
    {
        if (WallHp <= 0) return;
        Debug.Log(WallHp);
        WallHp -= damage;
        Debug.Log(WallHp);
        OnWallHpChanged?.Invoke();
        if (WallHp <= 0)
        {
            //バリケードが壊れたらサウンドを再生
            AudioSource.PlayClipAtPoint(destroySound.clip, transform.position);
            Debug.Log("ゲームオーバー");
            // ゲームオーバー処理をここに記述
            // GameManager.Instance.GameOver();

            //gameoverCanvasを表示する
            GameOverCanvas.SetActive(true);

            //ゲームをポーズする
            Time.timeScale = 0f;

            //continue?
            IsRetryCanvas.SetActive(true);
        }
    }

    //最大値を増やせる処理
    public void wallCustom(float maxUp)
    {
        WallMaxHp += maxUp;
        WallHp += maxUp;
        OnWallHpChanged?.Invoke();
    }

    //最大値を増やすときに乗算にできるオーバーロード
    public void wallCustom(float maxUp, bool multiplication)
    {
        if (multiplication)
        {
            WallMaxHp *= maxUp;
            WallHp *= maxUp;
            OnWallHpChanged?.Invoke();
        }
        else wallCustom(maxUp);
    }

    //壁を回復するだけの処理
    public void wallRecover(float heal)
    {
        WallHp += heal;
        WallHp = Mathf.Min(WallHp, WallMaxHp);
        OnWallHpChanged?.Invoke();
    }
}

