using TMPro;
using UnityEngine;

public class CoinsText : MonoBehaviour
{
    private TextMeshProUGUI _coinsValueText;

    private void Awake()
    {
        _coinsValueText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {

        _coinsValueText.text = PlayerData.Instance?.GetCoins().ToString();

        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnCoinsValueChanged += PlayerData_OnCoinsValueChanged;
        }
    }
    private void OnDestroy()
    {
        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnCoinsValueChanged -= PlayerData_OnCoinsValueChanged;
        }
    }

    private void PlayerData_OnCoinsValueChanged()
    {
        _coinsValueText.text = PlayerData.Instance?.GetCoins().ToString();
    }

}
