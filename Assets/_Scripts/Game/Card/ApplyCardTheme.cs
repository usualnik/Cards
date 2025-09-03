using UnityEngine;

public class ApplyCardTheme : MonoBehaviour
{
    ///private Material _material;
    private MeshRenderer _decalRenderer;
    private Card _card;

    private void Awake()
    {        
        _card = GetComponent<Card>();
    }

    private void Start()
    {

        _decalRenderer = GetComponentInChildren<Decal>().GetDecalMeshRenderer();


        switch (_card.GetCardDataSO().Title)
        {
            case "RedCard":
                _decalRenderer.material.mainTexture = ColorThemeManager.Instance.GetCurrentThemeSO().Sprites[0].texture;
                break;
            case "GreenCard":
                _decalRenderer.material.mainTexture = ColorThemeManager.Instance.GetCurrentThemeSO().Sprites[1].texture;
                break;
            case "BlueCard":
                _decalRenderer.material.mainTexture = ColorThemeManager.Instance.GetCurrentThemeSO().Sprites[2].texture;
                break;
            case "YellowCard":
                _decalRenderer.material.mainTexture = ColorThemeManager.Instance.GetCurrentThemeSO().Sprites[3].texture;
                break;
            case "PinkCard":
                _decalRenderer.material.mainTexture = ColorThemeManager.Instance.GetCurrentThemeSO().Sprites[4].texture;
                break;
            case "CyanCard":
                _decalRenderer.material.mainTexture = ColorThemeManager.Instance.GetCurrentThemeSO().Sprites[5].texture;
                break;
            default:
                break;
        }
    }
}
