using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour
{
    // パーティクルシステム
    ParticleSystem ps;

    [Header("グリッド設定")]
    // 発生させる列数（X軸方向）
    public int columns = 5;
    // 発生させる行数（Y軸方向）
    public int rows = 5;
    // 列間の距離
    public float columnSpacing = 1.5f;
    // 行間の距離
    public float rowSpacing = 1.0f;
    // 列と列の間の遅延時間
    public float delayBetweenColumns = 0.5f;

    [Header("発生位置")]
    // グリッド全体の開始位置（2D用）
    public Vector2 startPosition = new Vector2(10, 0);
    // 3Dと区別するためにz座標を設定
    public float zPosition = 0;

    void Start()
    {
        if (ps == null)
        {
            ps = GetComponent<ParticleSystem>();
        }

        var emission = ps.emission;
        emission.rateOverTime = 0;

        StartCoroutine(SpawnParticles());
    }

    IEnumerator SpawnParticles()
    {
        // コルーチン開始直後に1フレーム待機し、処理の安定化を図る
        yield return null;

        // 2重ループで列と行を生成
        for (int i = 0; i < columns; i++) // X軸方向（列）
        {
            // 列のX座標を計算
            float xPos = startPosition.x - i * columnSpacing;

            // 内側のループで、1列分のパーティクルを1つずつ発生させる
            for (int j = 0; j < rows; j++) // Y軸方向（行）
            {
                // Y座標を計算
                float yPos = startPosition.y + j * rowSpacing - (rows - 1) * rowSpacing / 2.0f;

                // 粒子を発生させる2D位置を設定
                Vector3 emitPosition = new Vector3(xPos, yPos, zPosition);

                var emitParams = new ParticleSystem.EmitParams();
                emitParams.position = emitPosition;

                // パーティクルを1つずつ、1フレーム内で発生させる
                ps.Emit(emitParams, 1);
            }


            // 列全体が発生し終わった後に、次の列に移るまでの遅延
            yield return new WaitForSeconds(delayBetweenColumns);
        }
        yield return new WaitForSeconds(2.0f);
        MyDestroy();
    }

    void MyDestroy()
    {
        Destroy(gameObject);
    }
}

