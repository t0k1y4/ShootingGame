using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float time = 0;
    public float maxGenTime = 2f;
    public float genTime;
    public float a;  // 減少量
    int num;

    void Start()
    {
        genTime = maxGenTime;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (genTime < 1.5f)
        {
            num = Random.Range(0, 3);
        }
        else
        {
            num = Random.Range(0, 2);
        }
        if (time > genTime)
        {
            float randomY = Random.Range(-4f, 2.0f);
            Vector3 spawnPosition = new Vector3(-12f, randomY, 0f);
            Instantiate(enemyPrefabs[num], spawnPosition, Quaternion.identity);
            time = 0;
        }
    }

    public void ChangeGenTime(int difficalty)
    {
        genTime = maxGenTime / (a * difficalty + 1f);

        if (difficalty % 10 == 0)
        {
            num = 3;
            Vector3 spawnPosition = new Vector3(-12f, 0f, 0f);
            Instantiate(enemyPrefabs[num], spawnPosition, Quaternion.identity);
        } 

    }
    
}
