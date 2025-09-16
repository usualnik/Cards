using UnityEngine;

[CreateAssetMenu(fileName = "ColorSO", menuName = "ColorData")]
public class ColorSO : ScriptableObject
{
    public string Name;
    public string RuName;
    public int Index;
    public int Price;
    public Color[] Colors;

    [Space(20f)]
    [Header("Envir Colors")]
    public Color BigPropsColor;
    public Color MiddlePropsRibbonBufferColor;
    public Color SmallPropsMetalColor;
    public Color FloorColor;
    public Color SendingRecievingLanesColor;
}
