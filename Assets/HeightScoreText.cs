using UnityEngine;
using TMPro;
    
public class HeightScoreText : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        //現在の最高記録を受け取り、ハイスコアに代入
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "HighScore: " + highScore +" kills!";
    }

}
