using TMPro;
using UnityEngine;

public class HeartsTimer : MonoBehaviour
{
    public static HeartsTimer Instance {  get; private set; }
    //private TextMeshProUGUI _heartsTimerText;
    private float _heartsTimer;

    private const float HEARTS_TIMER_MAX = 300f;
    
    private bool _heartsTimerRunning = false;

    private const int MAX_HEARTS_TO_START_UPDATE = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        gameObject.transform.SetParent(null, false);

        DontDestroyOnLoad(gameObject);

        _heartsTimer = HEARTS_TIMER_MAX;
    }

    private void Start()
    {
        PlayerData.Instance.OnHeartsValueChanged += PlayerData_OnHeartsValueChanged;

    }
    private void OnDestroy()
    {
        PlayerData.Instance.OnHeartsValueChanged -= PlayerData_OnHeartsValueChanged;
    }

    private void PlayerData_OnHeartsValueChanged()
    {
        if (PlayerData.Instance.GetHearts() < MAX_HEARTS_TO_START_UPDATE)
            _heartsTimerRunning = true;
    }

    private void Update()
    {
        if (!_heartsTimerRunning) return;

        UpdateHeartsTimer();

    }

    private void UpdateHeartsTimer()
    {

        _heartsTimer -= Time.deltaTime;

        //_heartsTimerText.text = "+1 in "_heartsTimer.ToString();

        if (_heartsTimer <= 0 && PlayerData.Instance.GetHearts() < MAX_HEARTS_TO_START_UPDATE)
        {
            PlayerData.Instance.SetHearts(PlayerData.Instance.GetHearts() + 1);
            _heartsTimer = HEARTS_TIMER_MAX;
        }
    }

    public string GetFormattedProgress() 
    {
        var minutes = Mathf.FloorToInt(_heartsTimer / 60);
        var seconds = Mathf.FloorToInt(_heartsTimer % 60);

        return string.Format("{0} : {1}" , minutes, seconds);
    }
    public bool GetIsRunning() => _heartsTimerRunning;
}
