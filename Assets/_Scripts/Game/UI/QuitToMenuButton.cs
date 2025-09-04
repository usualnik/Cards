using UnityEngine;
using UnityEngine.UI;

public class QuitToMenuButton : MonoBehaviour
{
    private Button quitButton;

    private void Awake()
    {
        quitButton = GetComponent<Button>();
    }

    private void Start()
    {
        quitButton.onClick.AddListener(QuitToMenu);
    }

    private void QuitToMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
