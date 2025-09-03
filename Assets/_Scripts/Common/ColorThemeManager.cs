using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ColorThemeManager : MonoBehaviour
{
    public static ColorThemeManager Instance;

    [Header("CURRENT CHOSEN COLOR | THEME")]
    [SerializeField] private ColorSO _currentColor;
    [SerializeField] private ThemeSO _currentTheme;

    [Header("OPENED COLORS | THEMES")]

    [SerializeField] private List<ThemeSO> _openedThemesList;
    [SerializeField] private List<ColorSO> _openedColorsList;


    [Header("ALL COLORS AND THEMES")]
    [SerializeField] private List<ColorSO> _colorsList;
    [SerializeField] private List<ThemeSO> _themeList;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


        gameObject.transform.SetParent(null);


        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        InitOpenedThemes();
        InitOpenedColors();

        _currentTheme = _themeList[PlayerData.Instance.GetCurrentChosenThemeIndex()];
        _currentColor = _colorsList[PlayerData.Instance.GetCurrentChosenColorIndex()];
    }

    private void InitOpenedThemes()
    {
        var savedOpenedThemesIndexes = PlayerData.Instance.GetOpenedThemesIndexesList();

        foreach (var themeIndex in savedOpenedThemesIndexes)
        {
            _openedThemesList.Add(_themeList[themeIndex]);
        }
    }
    private void InitOpenedColors()
    {
        var savedOpenedColorsIndexes = PlayerData.Instance.GetOpenedColorsIndexesList();

        foreach (var colorIndex in savedOpenedColorsIndexes)
        {
            _openedColorsList.Add(_colorsList[colorIndex]);
        }
    }


    public void SetCurrentColorSO(ColorSO color)
    {
        _currentColor = color;

        PlayerData.Instance.SetCurrentChosenColorIndex(color.Index);
    }
    public void SetCurrentThemeSO(ThemeSO theme)
    {
        _currentTheme = theme;

        PlayerData.Instance.SetCurrentChosenThemeIndex(theme.Index);
    }

    public ColorSO GetCurrentColorSO() => _currentColor;
    public ThemeSO GetCurrentThemeSO() => _currentTheme;

    public List<ColorSO> GetColorsList() => _colorsList;
    public List<ThemeSO> GetThemesList() => _themeList;

    public List<ThemeSO> GetOpenedThemes() => _openedThemesList;
    public List<ColorSO> GetOpenedColors() => _openedColorsList;


    public void UnlockTheme(int index)
    {
        _currentTheme = _themeList[index];

        _openedThemesList.Add(_themeList[index]);
        

        //Save
        PlayerData.Instance.SetOpenedThemeIndex(index);
        PlayerData.Instance.SetCurrentChosenThemeIndex(index);

        
    }
    public void UnlockColor(int index)
    {
        _currentColor = _colorsList[index];

        _openedColorsList.Add(_colorsList[index]);

       //Save
       PlayerData.Instance.SetOpenedColorIndex(index);
       PlayerData.Instance.SetCurrentChosenColorIndex(index);
    }
}
