using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _loseGameScreen;
    [SerializeField] private GameObject _winGameScreen;

    private void Start()
    {
        Buffer.Instance.OnBufferFull += Buffer_OnBufferFull;
        GameManager.Instance.OnWinGame += GameManager_OnWinGame;
    }

  
    private void OnDestroy()
    {
        Buffer.Instance.OnBufferFull -= Buffer_OnBufferFull;
        GameManager.Instance.OnWinGame += GameManager_OnWinGame;
    }

    private void Buffer_OnBufferFull()
    {
        AudioManager.Instance.Play("Lose");
        _loseGameScreen.SetActive(true);
    }
    private void GameManager_OnWinGame()
    {
        AudioManager.Instance.Play("Win");
        _winGameScreen.SetActive(true);
    }

}
