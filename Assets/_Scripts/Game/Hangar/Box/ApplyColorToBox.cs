using UnityEngine;

public class ApplyColorToBox : MonoBehaviour
{
    private MeshRenderer _renderer;
    private BoxTriggerHandler _handler; 

    private void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        _handler = GetComponentInChildren<BoxTriggerHandler>();
    }

    private void Start()
    {
        switch (_handler.GetWaitingForCardType())
        {
            case BoxTriggerHandler.WaitingForCard.Red:
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[0];
                break;
            case BoxTriggerHandler.WaitingForCard.Green: 
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[1];
                break;
            case BoxTriggerHandler.WaitingForCard.Blue:
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[2];
                break;
            case BoxTriggerHandler.WaitingForCard.Yellow:
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[3];
                break;
            case BoxTriggerHandler.WaitingForCard.Pink:
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[4];
                break;
            case BoxTriggerHandler.WaitingForCard.Cyan:
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[5];
                break;
            default:
                break;
        }
    }
}
