using TMPro;
using UnityEngine;
using YG;

public class NextPrizeTimer : MonoBehaviour
{
    private TextMeshProUGUI _nextPrizeTimer;

    private void Awake()
    {
        _nextPrizeTimer = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        ShowNextTimerTextFormatted();
    }

    private void ShowNextTimerTextFormatted()
    {
        
        if (ActivityPrizesHandler.Instance == null ||
            ActivityPrizesTimer.Instance == null ||
            ActivityPrizesHandler.Instance.GetFirstActivityPrize() == null)
        {
            _nextPrizeTimer.text = "-- : --";
            return;
        }

        var nextPrize = ActivityPrizesHandler.Instance.GetFirstActivityPrize();
              
       
        float timeRemaining = nextPrize.GetClaimedTimer() - ActivityPrizesTimer.Instance.ActivityTimer;
               
        if (timeRemaining <= 0)
        {
            _nextPrizeTimer.text = YG2.envir.language == "ru" ? "ÇÀÁÐÀÒÜ" : "CLAIM";
            return;
        }

        var minutes = Mathf.FloorToInt(timeRemaining / 60);
        var seconds = Mathf.CeilToInt(timeRemaining % 60);
                
        if (seconds == 60)
        {
            seconds = 0;
            minutes++;
        }

        _nextPrizeTimer.text = string.Format("{0:D2} : {1:D2}", minutes, seconds);
    }
}