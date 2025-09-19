using UnityEngine;

public class ColorizeObject : MonoBehaviour
{
    private enum ObjectType
    {
        EnvironmentBright,
        EnvironmentDark,
        SecondaryEnvironment,
        Floor,
        Rubber,
        Metal
    }
    [SerializeField] private ObjectType _type;
    private MeshRenderer _meshRenderer;


    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    void Start()
    {
        switch (_type)
        {
            //1
            case ObjectType.EnvironmentDark:
                _meshRenderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().EnvironmentDark;
                break;
            //2
            case ObjectType.EnvironmentBright:             
                 _meshRenderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().EnvironmentBright;
                break;
            //3
            case ObjectType.SecondaryEnvironment:           
                 _meshRenderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().SecondaryEnvironment;
                break;
            //4
            case ObjectType.Floor:
                _meshRenderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Floor;
                break;
            //5
            case ObjectType.Rubber:
                _meshRenderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Rubber;
                break;
            //6
            case ObjectType.Metal:
                _meshRenderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().Metal;
                break; 
            default:
                break;
        }
    }


}
