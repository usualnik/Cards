using TMPro;
using UnityEngine;

public class SkipsText : MonoBehaviour
{
    private TextMeshProUGUI _skipsValueText;

    private void Awake()
    {
        _skipsValueText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {

        _skipsValueText.text = PlayerData.Instance?.GetSkips().ToString();

        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnSkipsValueChanged += PlayerData_OnSkipsValueChanged;
        }
    }
    private void OnDestroy()
    {
        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnSkipsValueChanged -= PlayerData_OnSkipsValueChanged;
        }
    }

    private void PlayerData_OnSkipsValueChanged()
    {
        _skipsValueText.text = PlayerData.Instance?.GetSkips().ToString();
    }
}
