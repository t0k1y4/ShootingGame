using Unity.VisualScripting;
using UnityEngine;

public class Close : MonoBehaviour
{

    public ShopOpener smg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void  OnClick()
    {
        smg.Shoping();
    }
}
