using UnityEngine;

public class BarricadeController : MonoBehaviour
{
    public float hp;
    public float maxHp = 100f;

    void Start()
    {
        hp = maxHp;
        Debug.Log(hp + "/" + maxHp);
    }

    void Update()
    {

    }

    public void ChangeHealth(float amount)
    {
        // 体力の増減
        hp = Mathf.Clamp(hp + amount, 0, maxHp);
        Debug.Log(hp + "/" + maxHp);
    }
    
}
