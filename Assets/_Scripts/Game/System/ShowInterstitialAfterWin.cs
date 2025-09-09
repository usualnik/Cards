using UnityEngine;
using YG;

public class ShowInterstitialAfterWin : MonoBehaviour
{
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
        YG2.InterstitialAdvShow();
    }


}
