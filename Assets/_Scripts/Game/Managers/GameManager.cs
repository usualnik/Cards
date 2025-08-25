using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        Time.timeScale = 0f;
    }
}
