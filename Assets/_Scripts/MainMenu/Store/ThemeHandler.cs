using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ThemeHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _themeName;
    [SerializeField] private TextMeshProUGUI _themePriceText;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _buyButton;

    private int _themeIndex;
    private List<ThemeSO> _themes;

    private const int INSUFFICIENT_RESOURCES_WINDOW_INDEX = 3;

    private void Start()
    {
        _themes = ColorThemeManager.Instance.GetThemesList();

        _nextButton?.onClick.AddListener(ShowNextTheme);
        _prevButton?.onClick.AddListener(ShowPrevTheme);
        _buyButton?.onClick.AddListener(BuyTheme);

        Invoke("ShowCurrentChosenTheme",0.1f);

    }
    private void OnDestroy()
    {
        _nextButton?.onClick.RemoveListener(ShowNextTheme);
        _prevButton?.onClick.RemoveListener(ShowPrevTheme);
        _buyButton?.onClick.RemoveListener(BuyTheme);
    }

    

    private void OnEnable()
    {
        //ShowCurrentChosenTheme();
    }
    private void ShowCurrentChosenTheme()
    {
        _themeIndex = ColorThemeManager.Instance.GetCurrentThemeSO().Index;

        _themeName.text = YG2.envir.language == "ru" ? ColorThemeManager.Instance.GetCurrentThemeSO().RuName
            : ColorThemeManager.Instance.GetCurrentThemeSO().Name;

        _themePriceText.text = string.Empty;
    }

    private void ShowNextTheme()
    {
       
        if (_themeIndex < ColorThemeManager.Instance.GetThemesList().Count - 1)
        {
            _themeIndex++;
            _themeName.text = YG2.envir.language == "ru" ? _themes[_themeIndex].RuName : _themes[_themeIndex].Name;
            _themePriceText.text = _themes[_themeIndex].Price.ToString();
        }

        // if already have that theme - show no price
        if (ColorThemeManager.Instance.GetOpenedThemes().Contains(_themes[_themeIndex]))
        {
            _themePriceText.text = string.Empty;
            ColorThemeManager.Instance.SetCurrentThemeSO(_themes[_themeIndex]);
        }
    }

    private void ShowPrevTheme()
    {  

        if (_themeIndex > 0)
        {
            _themeIndex--;
            _themeName.text = YG2.envir.language == "ru" ? _themes[_themeIndex].RuName : _themes[_themeIndex].Name;
            _themePriceText.text = _themes[_themeIndex].Price.ToString();
        }


        // if already have that theme - show no price
        if (ColorThemeManager.Instance.GetOpenedThemes().Contains(_themes[_themeIndex]))
        {
            _themePriceText.text = string.Empty;
            ColorThemeManager.Instance.SetCurrentThemeSO(_themes[_themeIndex]);
        }
    }

    private void BuyTheme()
    {
        if (_themes[_themeIndex].Price <= PlayerData.Instance.GetGems() &&
            !ColorThemeManager.Instance.GetOpenedThemes().Contains(_themes[_themeIndex]))   
        {
            PlayerData.Instance.SetGems(PlayerData.Instance.GetGems() - _themes[_themeIndex].Price);

            _themePriceText.text = string.Empty;
                        
            ColorThemeManager.Instance.UnlockTheme(_themeIndex);
        }else if(_themes[_themeIndex].Price > PlayerData.Instance.GetGems() 
            && !ColorThemeManager.Instance.GetOpenedThemes().Contains(_themes[_themeIndex]))
        {
            WindowManager.Instance.OpenWindow(INSUFFICIENT_RESOURCES_WINDOW_INDEX);
        }
        else
        {

        }
    }

}
