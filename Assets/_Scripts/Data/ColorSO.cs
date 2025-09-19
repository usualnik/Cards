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
    public Color EnvironmentBright;
    public Color EnvironmentDark;   
    public Color SecondaryEnvironment;
    public Color Floor;
    public Color Rubber;
    public Color Metal;

}
