using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour
{
    public static Buffer Instance { get; private set; }
    [SerializeField] private Transform _headPos;

    private List<Card> _cardsInBuffer = new List<Card>(); // MAX capacity - 20 card
    private Vector3 _bufferCardOffset = new Vector3(0.2f,0,0);


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of Buffer");
        }
    }

    void Start()
    {
        Dumper.Instance.OnDumpCard += Dumper_OnDumpCard;
    }
    private void OnDestroy()
    {
        Dumper.Instance.OnDumpCard -= Dumper_OnDumpCard;
    }

    private void Dumper_OnDumpCard(Card card)
    {
        PutCardInBuffer(card);
    }

    private void PutCardInBuffer(Card card)
    {        
        if (_cardsInBuffer.Count > 0)
        {
            Card lastCard = _cardsInBuffer[_cardsInBuffer.Count - 1];
            card.transform.position = lastCard.transform.position + _bufferCardOffset;
        }
        else 
        {
            card.transform.position = _headPos.position;
        }

        _cardsInBuffer.Add(card);
        card.ChangeCardState(Card.CardState.InBuffer);
    }
}
