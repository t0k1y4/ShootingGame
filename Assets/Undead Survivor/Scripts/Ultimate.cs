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
    public GameObject beam;
    public GameObject explosion;
    public CameraShaker cameraShaker;
    public AudioSource audioSource;
    public AudioClip cool;

    public void Special()
    {
        if (PlayerStats.Instance.IsCanUltimate())
        {
            if (PlayerStats.Instance.GetWeaponInt(ultimate) > 0 && PlayerStats.Instance.GetWeaponInt(ultimate) < 5)
            {

                Instantiate(ult1);
                cameraShaker.Shake();
                StartCoroutine(CoolTime());
            }
            else
            {
                StartCoroutine(Ultimate2());
                StartCoroutine(CoolTime());
            }

        }
    }



    IEnumerator CoolTime()
    {
        float coolTime = 30.0f - (PlayerStats.Instance.GetWeaponCount(ultimateRate.name) / 10);
        ultButton.interactable = false;
        ultimateImage.fillAmount = 0; // アニメーション開始前にfillAmountを0にリセット
        ultimateImage.DOFillAmount(1, coolTime)
            .SetEase(Ease.Linear);
        yield return new WaitForSeconds(coolTime);
        ultButton.interactable = true;
        audioSource.clip=cool;
        audioSource.PlayOneShot(cool);
    }

    IEnumerator Ultimate2()
    {
        Instantiate(beam);
        yield return new WaitForSeconds(0.7f);
        Instantiate(explosion);
        cameraShaker.Shake();
        yield return new WaitForSeconds(1.5f);
        Instantiate(ult1);
        cameraShaker.Shake();
    }
}
