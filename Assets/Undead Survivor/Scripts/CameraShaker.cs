using UnityEngine;
using DG.Tweening; // DOTweenを使うために必要

public class CameraShaker : MonoBehaviour
{
    // 揺れの持続時間
    public float duration = 0.5f; 
    // 揺れの強さ（上下方向のみに影響）
    public Vector3 strength = new Vector3(0, 0.5f, 0); 
    // 揺れの振動回数
    public int vibrato = 10;
    // 揺れのランダム性
    public float randomness = 90;

    // 画面を揺らすメソッド
    public void Shake()
    {
        // 既に揺れアニメーションが実行中であれば、一度停止する
        transform.DOKill(true);
        
        // ローカル座標で画面を揺らす
        transform.DOShakePosition(duration, strength, vibrato, randomness);
    }

    // 例：特定のキーで揺れをテスト
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shake();
        }
    }
}
