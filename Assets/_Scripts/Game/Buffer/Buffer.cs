using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour
{
    public static Buffer Instance { get; private set; }
    [SerializeField] private Transform _headPos;
    [SerializeField] private float _delayBetweenCards = 0.2f;

    [SerializeField] private List<Card> _cardsInBuffer = new List<Card>(); // MAX capacity - 20 card
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
        _cardsInBuffer.Add(card);
        card.ChangeCardState(Card.CardState.InBuffer);
    }

    public void SendPackToConveyor(Card clickedCard)
    {
        string targetCardName = clickedCard.GetCardDataSO().name;

        if (_cardsInBuffer.Contains(clickedCard))
        {
            StartCoroutine(SendMatchingCardsWithDelay(targetCardName));
        }
        else
        {
            Debug.LogWarning("Clicked card not found in any column");
        }
    }

    private IEnumerator SendMatchingCardsWithDelay(string targetName)
    {

        List<Card> cardsToSend = new List<Card>();

        foreach (var card in _cardsInBuffer)
        {
            if (card.GetCardDataSO().name == targetName)
            {
                
                cardsToSend.Add(card);
                
            }
        }

        foreach (var card in cardsToSend)
        {
            card.GetComponent<CardFromBufferAnimation>().SendCardToConveyorAnimation();
            _cardsInBuffer.Remove(card);

            yield return new WaitForSeconds(_delayBetweenCards);
        }
    }
    public int GetBufferCardsListCount() => _cardsInBuffer.Count;
    public List<Card> GetBufferCardsList() => _cardsInBuffer;

}
