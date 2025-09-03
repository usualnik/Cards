using UnityEngine;

[CreateAssetMenu(fileName = "ColorSO", menuName = "ColorData")]
public class ColorSO : ScriptableObject
{
    public string Name;
    public string RuName;
    public int Index;
    public int Price;
    public Color[] Colors;
}
