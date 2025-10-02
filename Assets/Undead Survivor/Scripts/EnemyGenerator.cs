using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float time=0;
    public float genTime = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        int num = Random.Range(0, 2);
        if (time > genTime)
        {
            float randomY = Random.Range(-4f, 2.5f);
            Vector3 spawnPosition = new Vector3(-13f, randomY, 0f);
            Instantiate(enemyPrefabs[num], spawnPosition, Quaternion.identity);
            time = 0;
        }
    }
}
