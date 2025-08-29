using UnityEngine;
using UnityEngine.UI;

public class BuySkip : MonoBehaviour
{
    private Button _button;

    private const int COINS_AMOUNT_TO_BUY_SKIP = 50;
    private const int GEMS_AMOUNT_TO_BUY_SKIP = 5;


    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        _button.onClick.AddListener(Buy);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Buy);

    }

    private void Buy()
    {
        if (PlayerData.Instance.GetCoins() >= COINS_AMOUNT_TO_BUY_SKIP)
        {
            PlayerData.Instance.SetSkips(PlayerData.Instance.GetSkips() + 1);
            PlayerData.Instance.SetCoins(PlayerData.Instance.GetCoins() - COINS_AMOUNT_TO_BUY_SKIP);
        }
        else if(PlayerData.Instance.GetGems() >= GEMS_AMOUNT_TO_BUY_SKIP)
        {
            PlayerData.Instance.SetSkips(PlayerData.Instance.GetSkips() + 1);
            PlayerData.Instance.SetCoins(PlayerData.Instance.GetGems() - GEMS_AMOUNT_TO_BUY_SKIP);
        }
        else
        {
            // do nothing
        }
    }
}
