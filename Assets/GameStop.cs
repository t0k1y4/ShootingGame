using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStop : MonoBehaviour
{
    public GameObject pauseCanvas; // ポーズメニュー用キャンバス
    public GameObject stopButton;  // ストップボタン（再表示用）
    AudioSource stop; //ストップボタンのオーディオソース
    public AudioClip stopSound;  //ストップボタンを押したときの音
    public AudioClip closeSound; //再開＆タイトルへ戻るボタンを押したときの音

    void Start()
    {
        //Button btn = GetComponent<Button>();
        //btn.onClick.AddListener(PlayClickSound);
        stop = GetComponent<AudioSource>();
    }

    void PlayClickSound()
    {
        //stop.PlayOneShot(clickSound);
    }

    // ストップボタンを押したとき
    public void OpenStopButton()
    {
        stop.clip = stopSound;
        stop.Play();
        Time.timeScale = 0f; // ゲームを一時停止
        pauseCanvas.SetActive(true); // ポーズキャンバス表示
        stopButton.SetActive(false); // ストップボタンは非表示
    }

    // 再開ボタンを押したとき
    public void OnRetryYes()
    {
        stop.clip = closeSound;
        stop.Play();
        Time.timeScale = 1f; // ポーズ解除
        pauseCanvas.SetActive(false); // ポーズキャンバス非表示
        stopButton.SetActive(true); // ストップボタンを再表示
    }

    // タイトルへ戻るボタンを押したとき
    public void OnRetryNo()
    {
        stop.clip = closeSound;
        stop.Play();
        Time.timeScale = 1f; // ポーズ解除
        PlayerStats.Instance.ResetData(); // ステータス初期化
        SceneManager.LoadScene("StartScene"); // タイトルへ遷移
    }
}