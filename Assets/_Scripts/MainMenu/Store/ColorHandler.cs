using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ColorHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _colorName;
    [SerializeField] private TextMeshProUGUI _colorPriceText;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _buyButton;

    private int _colorIndex;
    private List<ColorSO> _colors;

    private const int INSUFFICIENT_RESOURCES_WINDOW_INDEX = 3;

    private void Start()
    {
        _colors = ColorThemeManager.Instance.GetColorsList();

        _nextButton?.onClick.AddListener(ShowNextColor);
        _prevButton?.onClick.AddListener(ShowPrevColor);
        _buyButton?.onClick.AddListener(BuyColor);

        Invoke("ShowCurrentChosenColor", 0.1f);
                
    }


    private void OnDestroy()
    {
        _nextButton?.onClick.RemoveListener(ShowNextColor);
        _prevButton?.onClick.RemoveListener(ShowPrevColor);
        _buyButton?.onClick.RemoveListener(BuyColor);
    }

    private void OnEnable()
    {
       // ShowCurrentChosenColor();
    }

    private void ShowCurrentChosenColor()
    {
        _colorIndex = ColorThemeManager.Instance.GetCurrentColorSO().Index;

        _colorName.text = YG2.envir.language == "ru" ? ColorThemeManager.Instance.GetCurrentColorSO().RuName 
            : ColorThemeManager.Instance.GetCurrentColorSO().Name;

        _colorPriceText.text = string.Empty;
    }


    private void ShowNextColor()
    {

        if (_colorIndex < ColorThemeManager.Instance.GetColorsList().Count - 1)
        {
            _colorIndex++;

            _colorName.text = YG2.envir.language == "ru" ? _colors[_colorIndex].RuName : _colors[_colorIndex].Name;

            _colorPriceText.text = _colors[_colorIndex].Price.ToString();
        }

        // if already have that color - show no price
        if (ColorThemeManager.Instance.GetOpenedColors().Contains(_colors[_colorIndex]))
        {
            _colorPriceText.text = string.Empty;
            ColorThemeManager.Instance.SetCurrentColorSO(_colors[_colorIndex]);
        }
    }

    private void ShowPrevColor()
    {

        if (_colorIndex > 0)
        {
            _colorIndex--;
            _colorName.text = YG2.envir.language == "ru" ? _colors[_colorIndex].RuName : _colors[_colorIndex].Name;
            _colorPriceText.text = _colors[_colorIndex].Price.ToString();
        }


        // if already have that theme - show no price
        if (ColorThemeManager.Instance.GetOpenedColors().Contains(_colors[_colorIndex]))
        {
            _colorPriceText.text = string.Empty;
            ColorThemeManager.Instance.SetCurrentColorSO(_colors[_colorIndex]);
        }
    }

    private void BuyColor()
    {
        if (_colors[_colorIndex].Price <= PlayerData.Instance.GetCoins() &&
            !ColorThemeManager.Instance.GetOpenedColors().Contains(_colors[_colorIndex]))
        {
            PlayerData.Instance.SetCoins(PlayerData.Instance.GetCoins() - _colors[_colorIndex].Price);

            _colorPriceText.text = string.Empty;

            ColorThemeManager.Instance.UnlockColor(_colorIndex);
        } else if (_colors[_colorIndex].Price >= PlayerData.Instance.GetCoins() &&
            !ColorThemeManager.Instance.GetOpenedColors().Contains(_colors[_colorIndex]))
        {
            WindowManager.Instance.OpenWindow(INSUFFICIENT_RESOURCES_WINDOW_INDEX);
        }
        else
        {

        }
    }
}
