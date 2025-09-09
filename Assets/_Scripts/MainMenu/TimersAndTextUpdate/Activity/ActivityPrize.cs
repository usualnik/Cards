using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;


public class ActivityPrize : MonoBehaviour, IPointerClickHandler
{
    public enum RewardType
    {
        Coin,
        Gem,
        Skip,
        LootboxColor,
        LootboxTheme
    }


    [Header("Props")]
    [SerializeField] private float _timeToBeClaimed;
    [SerializeField] private int _rewardAmount;
    [SerializeField] private RewardType _rewardType;
    


    [Header("Ref")]
    [SerializeField] private TextMeshProUGUI _claimedText;
   

    private bool _readyToBeClaimed = false;

    private void Update()
    {
        if (ActivityPrizesTimer.Instance.ActivityTimer >= _timeToBeClaimed)
        {
            _claimedText.text = YG2.envir.language == "ru" ? "ÇÀÁÐÀÒÜ" : "CLAIM";
            _readyToBeClaimed = true;
        }
        else
        {

            var minutes = Mathf.CeilToInt((ActivityPrizesTimer.Instance.ActivityTimer - _timeToBeClaimed) / 60);
            var seconds = Mathf.CeilToInt((ActivityPrizesTimer.Instance.ActivityTimer - _timeToBeClaimed) % 60);

            minutes = Mathf.Abs(minutes);
            seconds = Mathf.Abs(seconds);


            _claimedText.text = string.Format("{0:D2} : {1:D2}", minutes, seconds);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.Play("ButtonPress");

        if (_readyToBeClaimed)
        {

            ActivityPrizesHandler.Instance.RemoveActivityPrizeFromList(this);   

            GiveReward();

            gameObject.SetActive(false);
        }
    }

    private void GiveReward()
    {
        switch (_rewardType)
        {
            case RewardType.Coin:
                PlayerData.Instance.SetCoins(PlayerData.Instance.GetCoins() + _rewardAmount);
                break;
            case RewardType.Gem:
                PlayerData.Instance.SetGems(PlayerData.Instance.GetGems() + _rewardAmount);
                break;
            case RewardType.Skip:
                PlayerData.Instance.SetSkips(PlayerData.Instance.GetSkips() + _rewardAmount);
                break;
            case RewardType.LootboxColor:
                // add new color
                break;
            case RewardType.LootboxTheme:
                // add new theme
                break;
            default:
                break;
        }
    }

    public bool IsReadyToBeClaimed() => _readyToBeClaimed;
    public float GetClaimedTimer() => _timeToBeClaimed;
}
