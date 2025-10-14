using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Ultimate : MonoBehaviour
{
    public WeaponData ultimate;
    public WeaponData ultimateRate;
    public Image ultimateImage;
    public Button ultButton;
    public GameObject ult1;
    public void Spetial()
    {
        if (PlayerStats.Instance.IsCanUltimate())
        {
            switch (PlayerStats.Instance.GetWeaponInt(ultimate))
            {
                // case句の後にコロン(:)を記述
                case 1:
                    Instantiate(ult1);
                    StartCoroutine(CoolTime());
                    break;
                case 2:
                    Instantiate(ult1);
                    StartCoroutine(CoolTime());
                    break;
            }
        }

    }



    IEnumerator CoolTime()
    {
        float coolTime = 1.6f - (PlayerStats.Instance.GetWeaponCount(ultimateRate.name) / 10);
        ultButton.interactable = false;
        ultimateImage.DOFillAmount(1, coolTime)
    .From(0) // 0から開始
    .SetEase(Ease.Linear);
        yield return new WaitForSeconds(coolTime);
        ultButton.interactable = true;
    }
}