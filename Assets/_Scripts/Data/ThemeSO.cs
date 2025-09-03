using UnityEngine;

[CreateAssetMenu(fileName = "ThemeSO", menuName = "ThemeData")]
public class ThemeSO : ScriptableObject
{
    public string Name;
    public string RuName;
    public int Index;
    public int Price;
    public Sprite[] Sprites;
}
