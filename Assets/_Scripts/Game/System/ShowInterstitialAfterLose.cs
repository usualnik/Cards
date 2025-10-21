using System;
using UnityEngine;
using YG;

public class ShowInterstitialAfterLose : MonoBehaviour
{ 

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

        DateTime now = DateTime.Now;
        DateTime showAfterDate = new DateTime(2025, 10, 23);

        if (now >= showAfterDate)
        {
            YG2.InterstitialAdvShow();
        }

    }


}
