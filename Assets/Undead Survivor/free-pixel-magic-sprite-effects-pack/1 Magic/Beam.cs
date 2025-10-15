using UnityEngine;
using DG.Tweening; // DOTweenを使用するために必要

public class Beam : MonoBehaviour
{
    void Start()
    {
        // DORotateメソッドでZ軸の回転アニメーションを設定
        transform.DORotate(
            new Vector3(0, 0, -360), // 
            2f // アニメーションにかける時間（1秒）
        )
        .SetEase(Ease.InOutQuad); // 加速するイージングタイプを設定
    }
}