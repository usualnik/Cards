using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;


[System.Serializable]
public class Data
{
    public int Coins = 0;
    public int Gems = 0;
    public int Hearts = 5;
    public int Skips = 0;
    public int SkipsProgress = 1;
    public int CurrentLevel = 1;

    public List<int> _openedThemesIndexes = new List<int>();
    public List<int> _openedColorsIndexes = new List<int>();
    public int CurrentChosenThemeIndex = 0;
    public int CurrentChosenColorIndex = 0;
}

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }
    
    public event Action OnCoinsValueChanged;
    public event Action OnGemsValueChanged;
    public event Action OnHeartsValueChanged;
    public event Action OnSkipsValueChanged;
    public event Action OnCurrentLevelChanged;
    public event Action OnSkipsProgressChanged;
   
    [SerializeField] private Data data;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameObject.transform.SetParent(null, false);
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        if (YG2.saves.YandexServerData != null && YG2.saves.YandexServerData != data)           
        {
           LoadPlayerData();
        }
        else
        {
           InitFirstTimePlayingData();
        }
    }

    private void InitFirstTimePlayingData()
    {
        //build new data
        data = new Data();

        //Add default theme and color
        data._openedColorsIndexes.Add(0);
        data._openedThemesIndexes.Add(0);
    }

    private void LoadPlayerData()
    {
        this.data = YG2.saves.YandexServerData;
    }
    private void SavePlayerDataToYandex()
    {
        YG2.saves.YandexServerData = this.data;
        YG2.SaveProgress();
    }

    #region Get
    public int GetCoins() => data.Coins;
    public int GetGems() => data.Gems;
    public int GetHearts() => data.Hearts;
    public int GetSkips() => data.Skips;
    public int GetCurrentLevel() => data.CurrentLevel;
    public int GetSkipsProgress() => data.SkipsProgress;
    public List<int> GetOpenedThemesIndexesList() => data._openedThemesIndexes;
    public List<int> GetOpenedColorsIndexesList() => data._openedColorsIndexes;
    public int GetCurrentChosenThemeIndex() => data.CurrentChosenThemeIndex;
    public int GetCurrentChosenColorIndex() => data.CurrentChosenColorIndex;

    #endregion

    #region Set

    public void SetCoins(int value)
    {
        var temp = data.Coins;     
        
        data.Coins = value;

        if (data.Coins < 0)
        {
            data.Coins = temp;
        }
        else
        {
            SavePlayerDataToYandex();
            OnCoinsValueChanged?.Invoke(); 
        }
    }
    public void SetGems(int value)
    {
        var temp = data.Gems;

        data.Gems = value;
        if (data.Gems < 0)
        {
            data.Gems = temp;
        }
        else
        {
            SavePlayerDataToYandex();

            OnGemsValueChanged?.Invoke();
        }
       
    }
    public void SetHearts(int value)
    {
        var temp = data.Hearts;

        data.Hearts = value;


        if (data.Hearts > 15)
        {
            data.Hearts = 15;
            OnCurrentLevelChanged?.Invoke();
            return;
        }

        if (data.Hearts < 0)
        {
            data.Hearts = temp;
        }
        else
        {
            SavePlayerDataToYandex();

            OnHeartsValueChanged?.Invoke();
        }


    }
    public void SetSkips(int value)
    {
        var temp = data.Skips;
        
        data.Skips = value;

        if (data.Skips < 0)
        { 
            data.Skips = temp;
        }else
        {
            SavePlayerDataToYandex();

            OnSkipsValueChanged?.Invoke();
        }        
    }

    public void SetSkipsProgress(int value)
    {
        data.SkipsProgress = value;

        SavePlayerDataToYandex();

        OnSkipsProgressChanged?.Invoke();
    }
    public void SetCurrentLevel(int value)
    {
        var temp = data.CurrentLevel;
        data.CurrentLevel = value;
       
        if (data.CurrentLevel < 0)
        {
            data.CurrentLevel = temp;
        }
        else
        {
            SavePlayerDataToYandex();

            OnCurrentLevelChanged?.Invoke();
        }   
         
        
    }

    public void SetOpenedThemeIndex(int openedThemeIndex)
    {
        data._openedThemesIndexes.Add(openedThemeIndex);
        SavePlayerDataToYandex();
    }
    public void SetOpenedColorIndex(int openedColorIndex)
    {
        data._openedColorsIndexes.Add(openedColorIndex);
        SavePlayerDataToYandex();
    }

    public void SetCurrentChosenThemeIndex(int currentChosenThemeIndex)
    {
        data.CurrentChosenThemeIndex = currentChosenThemeIndex;
        SavePlayerDataToYandex();
    }
    public void SetCurrentChosenColorIndex(int currentChosenColorIndex)
    {
        data.CurrentChosenColorIndex = currentChosenColorIndex;
        SavePlayerDataToYandex();
    }

    #endregion

}
