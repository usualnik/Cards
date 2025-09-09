using UnityEngine;

public class LevelReward : MonoBehaviour
{
	public static LevelReward Instance { get; private set; }

  

    public enum LevelRewardType
	{
		Coins,
		Gems,
		Skips
	}

	[SerializeField] private LevelRewardType _levelRewardType;

	[SerializeField] private int _levelRewardAmount;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one instance of LevelReward");
    }

    private void Start()
    {
        GameManager.Instance.OnWinGame += GameManager_OnWinGame;
    }   

    private void OnDestroy()
    {
        GameManager.Instance.OnWinGame -= GameManager_OnWinGame;
    }
    private void GameManager_OnWinGame()
    {
        GiveReward();
    }


    public void GiveReward()
    {
        switch (_levelRewardType)
        {
            case LevelRewardType.Coins:
                PlayerData.Instance.SetCoins(PlayerData.Instance.GetCoins() + _levelRewardAmount);
                break;
            case LevelRewardType.Gems:
                PlayerData.Instance.SetGems(PlayerData.Instance.GetGems() + _levelRewardAmount);
                break;
            case LevelRewardType.Skips:
                PlayerData.Instance.SetSkips(PlayerData.Instance.GetSkips() + _levelRewardAmount);
                break;
            default:
                break;
        }
    }

}
