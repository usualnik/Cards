using UnityEngine;
using UnityEngine.UI;
using YG;

public class WatchRewardedAds : MonoBehaviour
{
	public enum RewardType
	{
		Hearts,
		Coins,
		Gems,
		Skips

	}
	[SerializeField] private RewardType _rewardType;
    [SerializeField] private int _rewardAmount;

	private Button _rewardButton;

    private void Awake()
    {
        _rewardButton = GetComponent<Button>();
    }

    private void Start()
    {
        _rewardButton?.onClick.AddListener(GiveReward);
    }

    private void OnDestroy()
    {
        _rewardButton?.onClick.RemoveListener(GiveReward);
    }
    private void GiveReward()
	{
        switch (_rewardType)
        {
            case RewardType.Hearts:
                YG2.RewardedAdvShow(string.Empty,
                    () => PlayerData.Instance.SetHearts(PlayerData.Instance.GetHearts() + _rewardAmount));
                break;
            case RewardType.Coins:
                YG2.RewardedAdvShow(string.Empty,
                    () => PlayerData.Instance.SetCoins(PlayerData.Instance.GetCoins() + _rewardAmount));
                break;
            case RewardType.Gems:
                YG2.RewardedAdvShow(string.Empty,
                    () => PlayerData.Instance.SetGems(PlayerData.Instance.GetGems() + _rewardAmount));
                break;
            case RewardType.Skips:
                
                if (PlayerData.Instance.GetSkipsProgress() % 3 == 0)
                {
                    YG2.RewardedAdvShow(string.Empty,
                   () => PlayerData.Instance.SetSkips(PlayerData.Instance.GetSkips() + _rewardAmount));

                    //reset skip progress
                    PlayerData.Instance.SetSkipsProgress(1);
                }
                else
                {
                    YG2.RewardedAdvShow(string.Empty,
                  () => PlayerData.Instance.SetSkipsProgress(PlayerData.Instance.GetSkipsProgress() + 1));                   
                }

                    break;
            default:
                break;
        }
    }



}
