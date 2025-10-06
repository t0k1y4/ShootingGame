using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    float killed = 0;       // 敵を倒した数
    int difficalty = 0;     // 難易度
    public int diff = 10;   // 難易度の上昇量

    public GameObject wall;
    Wall wallScript;
    public GameObject eg;
    EnemyGenerator egScript;
    public TextMeshProUGUI killedText;
    public TextMeshProUGUI difficaltyText;
    public TextMeshProUGUI wallHpText;

    void Start()
    {
        wallScript = wall.GetComponent<Wall>();
        egScript = eg.GetComponent<EnemyGenerator>();
    }

    void Update()
    {
        // テキストの更新
        killedText.text = killed + ":killed !";
        difficaltyText.text = "difficalty:" + difficalty;
        wallHpText.text = "HP : " + wallScript.WallHp;
    }

    public void KilledCount()
    {
        // 敵を倒したときにスコアを加算
        killed += 1;
        Difficalty();
    }

    public void Difficalty()
    {
        // スコアがdiffの倍数になったときに難易度が上昇
        if (killed % diff == 0)
        {
            difficalty += 1;
            egScript.ChangeGenTime(difficalty);
        }
    }
    
}
