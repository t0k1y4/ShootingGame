using UnityEngine;

public class Supporters : MonoBehaviour
{
    public GameObject supporter1;
    public GameObject supporter2;
    public GameObject supporter3;

    void Update()
    {
        if (PlayerStats.Instance.GetSupporter(1))
        {
            supporter1.SetActive(true);
        }
        if (PlayerStats.Instance.GetSupporter(2))
        {
            supporter2.SetActive(true);
        }
        if (PlayerStats.Instance.GetSupporter(3))
        {
            supporter3.SetActive(true);
        }
    }
    
}
