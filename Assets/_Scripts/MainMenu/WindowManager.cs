using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Instance { get; private set; }

    [Header("Menu windows refs")]
    [SerializeField] private CanvasGroup[] _windowCanvasGroups;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of WindowManager");
        }
    }

    private void Start()
    {
        // Open main Menu
        OpenWindow(0);
    }

    public void OpenWindow(int index)
    {
        // Hide all windows but keep objects active
        foreach (var canvasGroup in _windowCanvasGroups)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        // Show selected window
        _windowCanvasGroups[index].alpha = 1;
        _windowCanvasGroups[index].interactable = true;
        _windowCanvasGroups[index].blocksRaycasts = true;
    }

    public CanvasGroup GetWindow(int index)
    {
        if (index >= 0 && index < _windowCanvasGroups.Length)
            return _windowCanvasGroups[index];
        return null;
    }
}