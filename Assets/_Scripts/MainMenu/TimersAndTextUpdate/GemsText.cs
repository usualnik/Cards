using TMPro;
using UnityEngine;

public class GemsText : MonoBehaviour
{
    private TextMeshProUGUI _gemsValueText;

    private void Awake()
    {
        _gemsValueText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {

        _gemsValueText.text = PlayerData.Instance?.GetGems().ToString();

        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnGemsValueChanged += PlayerData_OnGemsValueChanged;
        }
    }
    private void OnDestroy()
    {
        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnGemsValueChanged -= PlayerData_OnGemsValueChanged;
        }
    }

    private void PlayerData_OnGemsValueChanged()
    {
        _gemsValueText.text = PlayerData.Instance?.GetGems().ToString();
    }
}
