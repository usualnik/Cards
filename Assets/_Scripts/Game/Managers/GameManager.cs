using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public event Action OnWinGame;

    [SerializeField] private List<Card> _cardsOnScene = new List<Card>();
    [SerializeField] private List<Box> _boxOnScene = new List<Box>();

    private const int MAIN_MENU_BUILD_INDEX = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of game manager");
        }
    }

    private void Start()
    {
        Buffer.Instance.OnBufferFull += Buffer_OnBufferFull;

        InitWinCondition();
    }

    private void InitWinCondition()
    {
        _cardsOnScene = FindObjectsByType<Card>(FindObjectsSortMode.None).ToList();
        _boxOnScene = FindObjectsByType<Box>(FindObjectsSortMode.None).ToList();

        foreach (var card in _cardsOnScene)
        {
            card.OnCardDestroyed += Card_OnCardDestroyed;
        }

        foreach (var box in _boxOnScene)
        {
            box.OnDestroyBox += Box_OnDestroyBox;
        }

    }

    private void Box_OnDestroyBox(Box box)
    {
       box.OnDestroyBox -= Box_OnDestroyBox;
        _boxOnScene.Remove(box);

        CheckWinGame();
    }

    private void Card_OnCardDestroyed(Card card)
    {
        card.OnCardDestroyed -= Card_OnCardDestroyed;
        _cardsOnScene.Remove(card);

        CheckWinGame();
    }

    private void CheckWinGame()
    {
        if (_boxOnScene.Count <= 0 && _cardsOnScene.Count <= 0)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        OnWinGame?.Invoke();
        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        Buffer.Instance.OnBufferFull -= Buffer_OnBufferFull;
    }

    private void Buffer_OnBufferFull()
    {
        PlayerData.Instance.SetHearts(PlayerData.Instance.GetHearts() - 1);

        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        PlayerData.Instance.SetHearts(PlayerData.Instance.GetHearts() - 1);

        SceneManager.LoadScene(MAIN_MENU_BUILD_INDEX);
    }
}
