using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _loseGameScreen;

    private void Start()
    {
        Buffer.Instance.OnBufferFull += Buffer_OnBufferFull;
    }

    private void OnDestroy()
    {
        Buffer.Instance.OnBufferFull -= Buffer_OnBufferFull;
    }

    private void Buffer_OnBufferFull()
    {
        _loseGameScreen.SetActive(true);
    }
}
