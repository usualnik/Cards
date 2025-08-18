using System;
using UnityEngine;

public class Dumper : MonoBehaviour
{
    public static Dumper Instance { get; private set; }
    public event Action<Card> OnDumpCard;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of Dumper");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Card>(out Card card))
        {
            OnDumpCard?.Invoke(card);
            //Destroy(other.gameObject);
        }
    }
}
