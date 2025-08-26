using UnityEngine;

public class SetBackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _sceneBackgroundMusic;

    private void Start()
    {
        BackgroundMusic.Instance.SetBackgroundMusic(_sceneBackgroundMusic);
    }

}
