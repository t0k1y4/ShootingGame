using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    // startボタンを押したら、シーンを切り替る
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
