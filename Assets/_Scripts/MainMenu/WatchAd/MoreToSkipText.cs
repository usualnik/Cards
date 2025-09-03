using TMPro;
using UnityEngine;

public class MoreToSkipText : MonoBehaviour
{
    private TextMeshProUGUI _moreToSkipValue;
    private const int MAX_SKIP_PROGRESS = 4;

    private void Awake()
    {
        _moreToSkipValue = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        PlayerData.Instance.OnSkipsProgressChanged += PlayerData_OnSkipsProgressChanged;

        _moreToSkipValue.text = (MAX_SKIP_PROGRESS - PlayerData.Instance.GetSkipsProgress()).ToString();
    }
    private void OnDestroy()
    {
        PlayerData.Instance.OnSkipsProgressChanged -= PlayerData_OnSkipsProgressChanged;

    }

    private void PlayerData_OnSkipsProgressChanged()
    {
        _moreToSkipValue.text = (MAX_SKIP_PROGRESS - PlayerData.Instance.GetSkipsProgress()).ToString();
    }
}
