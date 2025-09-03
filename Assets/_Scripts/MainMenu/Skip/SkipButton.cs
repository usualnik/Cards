using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
    private Button _skipButton;
    private const int INSUFFICIENT_RESOURCES_WINDOW_INDEX = 3;
    
    private void Awake()
    {
        _skipButton = GetComponent<Button>();
    }
    private void Start()
    {
        _skipButton.onClick.AddListener(SkipLevel);

    }

    private void OnDestroy()
    {
        _skipButton.onClick.RemoveListener(SkipLevel);

    }
    private void SkipLevel()
    {
        if (PlayerData.Instance.GetSkips() > 0)
        {
            PlayerData.Instance.SetSkips(PlayerData.Instance.GetSkips() - 1);
            PlayerData.Instance.SetCurrentLevel(PlayerData.Instance.GetCurrentLevel() + 1);
        }
        else
        {
            WindowManager.Instance.OpenWindow(INSUFFICIENT_RESOURCES_WINDOW_INDEX);
        }
    }

}
