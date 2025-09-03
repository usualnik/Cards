using UnityEngine;

public class ApplyCardColor : MonoBehaviour
{
    //private Material _material;
    private MeshRenderer _renderer;
    private Card _card;

    private void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        _card = GetComponent<Card>();
    }

    private void Start()
    {
        switch (_card.GetCardDataSO().Title)
        {
            case "RedCard":
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[0];
                break;
            case "GreenCard":
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[1];
                break;
            case "BlueCard":
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[2];
                break;
            case "YellowCard":
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[3];
                break;
            case "PinkCard":
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[4];
                break;
            case "CyanCard":
                _renderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Colors[5];
                break;
            default:
                break;
        }
    }
}
