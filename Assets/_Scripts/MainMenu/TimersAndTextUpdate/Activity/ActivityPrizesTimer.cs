using UnityEngine;

public class ActivityPrizesTimer : MonoBehaviour
{
    public static ActivityPrizesTimer Instance {  get; private set; }
    public float ActivityTimer {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        gameObject.transform.SetParent(null, false);

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        ActivityTimer += Time.deltaTime;
    }

    
}
