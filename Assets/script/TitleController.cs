using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    private AudioSource start; // AudioSource
    public AudioClip startSound;   // 最初の音
    public AudioClip startSound2;  // 次の音

    private void Awake()
    {
        start = GetComponent<AudioSource>(); // AudioSource を取得
    }

    public void OnStartButtonClicked()
    {
        // 音を順番に鳴らす（同時再生も可能）
        if (start != null)
        {
            if (startSound != null) start.PlayOneShot(startSound);
            if (startSound2 != null) start.PlayOneShot(startSound2);
        }
        //
        StartCoroutine(GameStart());
        
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.5f);
        // シーン遷移（音を鳴らしてから遷移したいならコルーチンにする）
        SceneManager.LoadScene("SampleScene");

    }
}
