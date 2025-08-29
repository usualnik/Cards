using TMPro;
using UnityEngine;

public class CurrentLevelText : MonoBehaviour
{
    private TextMeshProUGUI _currentLevelText;

    private void Awake()
    {
        _currentLevelText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {

        _currentLevelText.text = PlayerData.Instance?.GetCurrentLevel().ToString();

        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnCurrentLevelChanged += PlayerData_OnCurrentLevelChanged;
        }
    }
    private void OnDestroy()
    {
        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnCurrentLevelChanged -= PlayerData_OnCurrentLevelChanged;
        }
    }

    private void PlayerData_OnCurrentLevelChanged()
    {
        _currentLevelText.text = PlayerData.Instance?.GetCurrentLevel().ToString();
    }
}
