using UnityEngine;
using YG;

public class ShowInterstitialAfterLose : MonoBehaviour
{
    private static int _loseCounter = 0;

    private void Start()
    {
        Buffer.Instance.OnBufferFull += Buffer_OnBufferFull;               
    }

   

    private void OnDestroy()
    {
        Buffer.Instance.OnBufferFull -= Buffer_OnBufferFull;

    }

    private void Buffer_OnBufferFull()
    {
        if (Buffer.Instance == null)
        {
            return;
        }

        _loseCounter++;
        
        bool shouldShowAdEveryLose = PlayerData.Instance.GetCurrentLevel() <= 5;

        if (shouldShowAdEveryLose)
        {
            YG2.InterstitialAdvShow();
        }
        else
        {
            if (_loseCounter % 2 == 0)
            {
                YG2.InterstitialAdvShow();
            }
        }    
    }


}
