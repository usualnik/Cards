using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WindowManager : MonoBehaviour
{
    public static WindowManager Instance { get; private set; }

    [Header("Menu windows refs")]
    [SerializeField] private GameObject[] _windows;


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
        //Open main Menu
        OpenWindow(0);

    }

    public void OpenWindow(int index)
    {
        //Close every other window
        foreach (var window in _windows)
        {
            window.gameObject.SetActive(false);
        }

        //Open one, that we need
        _windows[index].SetActive(true);
    }
}
