using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStop : MonoBehaviour
{
    public GameObject pauseCanvas; // ポーズメニュー用キャンバス
    public GameObject stopButton;  // ストップボタン（再表示用）

    // ストップボタンを押したとき
    public void OpenStopButton()
    {
        Time.timeScale = 0f; // ゲームを一時停止
        pauseCanvas.SetActive(true); // ポーズキャンバス表示
        stopButton.SetActive(false); // ストップボタンは非表示
    }

    // 再開ボタンを押したとき
    public void OnRetryYes()
    {
        Time.timeScale = 1f; // ポーズ解除
        pauseCanvas.SetActive(false); // ポーズキャンバス非表示
        stopButton.SetActive(true); // ストップボタンを再表示
    }

    // タイトルへ戻るボタンを押したとき
    public void OnRetryNo()
    {
        Time.timeScale = 1f; // ポーズ解除
        PlayerStats.Instance.ResetData(); // ステータス初期化
        SceneManager.LoadScene("StartScene"); // タイトルへ遷移
    }
}