using UnityEngine;
using DG.Tweening;

public class Beam : MonoBehaviour
{
    void Start()
    {
        // 1秒かけて現在の状態からZ軸を-180度（時計回り）回転させる
        transform.DORotate(
            new Vector3(0, 0, -180), // Z軸に-180度（時計回り）
            1f // アニメーションにかける時間
        )
        .SetEase(Ease.InOutQuad)
        .SetRelative(true)
        .OnComplete(() => {
            // アニメーション完了後にこのゲームオブジェクトを破棄する
            Destroy(gameObject);
        });
    }
}