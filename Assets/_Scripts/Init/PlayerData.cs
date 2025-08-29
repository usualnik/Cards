using System;
using System.Collections;
using UnityEngine;
//using YG;

[System.Serializable]
public class Data
{
    public int Coins;
    public int Gems;
    public int Hearts;
    public int Skips;
    public int CurrentLevel = 1;

}

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }
    
    public event Action OnCoinsValueChanged;
    public event Action OnGemsValueChanged;
    public event Action OnHeartsValueChanged;
    public event Action OnSkipsValueChanged;
    public event Action OnCurrentLevelChanged;

    public event Action OnDataLoaded;


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
        //if (YG2.saves.YandexServerData != null && YG2.saves.YandexServerData != data 
        //    && YG2.saves.YandexServerData.currentLevelIndex > 0) 
        //{
        //    SetPlayerData(YG2.saves.YandexServerData);
        //}

        //---------------------STUB--------------------
        StartCoroutine(SetPlayerDataWithDelay());
    }

    private IEnumerator SetPlayerDataWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        SetPlayerData(null);
    }

    private void SetPlayerData(Data yandexServerData)
    {
        //this.data = yandexServerData;
        OnDataLoaded?.Invoke();
    }
    private void SavePlayerDataToYandex()
    {
        //YG2.saves.YandexServerData = this.data;
        //YG2.SaveProgress();
    }

    #region Get
    public int GetCoins() => data.Coins;
    public int GetGems() => data.Gems;
    public int GetHearts() => data.Hearts;
    public int GetSkips() => data.Skips;
    public int GetCurrentLevel() => data.CurrentLevel;

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
            OnGemsValueChanged?.Invoke();
        }
       
    }
    public void SetHearts(int value)
    {
        var temp = data.Hearts;

        data.Hearts = value;
        
        if (data.Hearts < 0)
        {
            data.Hearts = temp;
        }
        else
        {
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
            OnSkipsValueChanged?.Invoke();
        }
        
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
            OnCurrentLevelChanged?.Invoke();
        }

        if (data.CurrentLevel >= 15)
        {
            data.CurrentLevel = 15;
            OnCurrentLevelChanged?.Invoke();
        }       
           
        
    }
    #endregion

}
