using TMPro;
using UnityEngine;

public class HeartsText : MonoBehaviour
{
    private TextMeshProUGUI _heartsValueText;

    private void Awake()
    {
        _heartsValueText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {

        _heartsValueText.text = PlayerData.Instance?.GetHearts().ToString();

        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnHeartsValueChanged += PlayerData_OnHeartsValueChanged;
        }
    }
    private void OnDestroy()
    {
        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnHeartsValueChanged -= PlayerData_OnHeartsValueChanged;
        }
    }

    private void PlayerData_OnHeartsValueChanged()
    {
        _heartsValueText.text = PlayerData.Instance?.GetHearts().ToString();
    }
}
