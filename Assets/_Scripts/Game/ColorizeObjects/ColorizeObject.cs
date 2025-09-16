using UnityEngine;

public class ColorizeObject : MonoBehaviour
{
    private enum ObjectType
    {
        BigProps,
        MiddleProps,
        SmallProps,
        Ribbon,
        Metal,
        Buffer,   
        Floor,
        SendingLane,
        RecievingLane
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
            case ObjectType.BigProps:
                _meshRenderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().BigPropsColor;

                break;

            //2
            case ObjectType.MiddleProps:               
            case ObjectType.Ribbon:                
            case ObjectType.Buffer:
                _meshRenderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().MiddlePropsRibbonBufferColor;

                break;
            
            //3
            case ObjectType.SmallProps:              
            case ObjectType.Metal:
                _meshRenderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().SmallPropsMetalColor;

                break;
           
            //4
            case ObjectType.Floor:
                _meshRenderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().FloorColor;

                break;

            //5
            case ObjectType.SendingLane:                
            case ObjectType.RecievingLane:
                _meshRenderer.material.color = ColorThemeManager.Instance.GetCurrentColorSO().SendingRecievingLanesColor;

                break;     
                

            default:
                break;
        }
    }


}
