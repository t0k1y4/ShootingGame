using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{

    TextMeshProUGUI uiText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        UIUpdater();
    }

    void OnEnable()
    {
        // PlayerStatsの武器変更イベントを購読し、UIUpdaterを呼び出す
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.OnMoneyChanged += UIUpdater;
        }
    }

        void OnDisable()
    {
        // PlayerStatsの武器変更イベントの購読を解除する
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.OnMoneyChanged -= UIUpdater;
        }
    }

    public void UIUpdater()
    {
        this.uiText.text = PlayerStats.Instance.money + "$";
    }

}
