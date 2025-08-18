using UnityEngine;

public class Receiver : MonoBehaviour
{
    public static Receiver Instance { get; private set; }      

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of Receiver");
        }
    }
}
