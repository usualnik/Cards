using TMPro;
using UnityEngine;

public class HeartsTimerText : MonoBehaviour
{
    private TextMeshProUGUI _heartsTimerText;
    private void Awake()
    {
        _heartsTimerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (HeartsTimer.Instance.GetIsRunning())
        {
            _heartsTimerText.text = "+1 in " + HeartsTimer.Instance.GetFormattedProgress();
        }
        else
        {
            _heartsTimerText.text = string.Empty;
        }
       
    }
}
