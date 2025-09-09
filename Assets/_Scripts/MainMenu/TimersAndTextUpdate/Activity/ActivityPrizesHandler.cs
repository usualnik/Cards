using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ActivityPrizesHandler : MonoBehaviour
{
    public static ActivityPrizesHandler Instance { get; private set; }

    [SerializeField] private List<ActivityPrize> _activityPrizesList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one instance of ActivityPrizeHandler");
    }

    public ActivityPrize GetFirstActivityPrize()
    {
        return _activityPrizesList[0];
    }
    public void RemoveActivityPrizeFromList(ActivityPrize activityPrize)
    {
        _activityPrizesList.Remove(activityPrize);        
    }
}
