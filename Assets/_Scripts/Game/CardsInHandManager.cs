using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsInHandManager : MonoBehaviour
{
    public static CardsInHandManager Instance { get; private set; }
    public event Action<List<Card>> OnSendCardsToConveyor;

    [SerializeField] private List<Card> _firstColumn;
    [SerializeField] private List<Card> _secondColumn;
    [SerializeField] private float _delayBetweenCards = 0.2f;
    [SerializeField] private Vector3 _cardSpacing = new Vector3(0, 0.2f, 0.1f); 
    [SerializeField] private Transform _firstColumnStartPoint; 
    [SerializeField] private Transform _secondColumnStartPoint;

   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of Card in hand manager");
        }

    }
    private void Start()
    {
        RearrangeCardsInColumn(_firstColumn,_firstColumnStartPoint);
        RearrangeCardsInColumn(_secondColumn, _secondColumnStartPoint);
    }

    public List<Card> GetFirstColumn() => _firstColumn;
    public List<Card> GetSecondColumn() => _secondColumn;

    public void SendPackToConveyor(Card clickedCard)
    {
        string targetCardName = clickedCard.GetCardDataSO().name;

       

        if (_firstColumn.Contains(clickedCard))
        {
            StartCoroutine(SendMatchingCardsWithDelay(_firstColumn, targetCardName, _firstColumnStartPoint));
        }
        else if (_secondColumn.Contains(clickedCard))
        {
            StartCoroutine(SendMatchingCardsWithDelay(_secondColumn, targetCardName, _secondColumnStartPoint));
        }
        else
        {
            Debug.LogWarning("Clicked card not found in any column");
        }
    }

    private IEnumerator SendMatchingCardsWithDelay(List<Card> column, string targetName, Transform columnStartPoint)
    {
        List<Card> cardsToSend = new List<Card>();

        // �������� ����� ��� ��������
        foreach (var card in column)
        {
            if (card.GetCardDataSO().name == targetName && Receiver.Instance.GetCanAcceptMore(cardsToSend.Count))
            {
                cardsToSend.Add(card);
                card.ChangeCardState(Card.CardState.OnConveyor);
            }
            else
            {
                break;
            }
        }

        OnSendCardsToConveyor?.Invoke(cardsToSend);

        // ���������� ����� �� ��������
        foreach (var card in cardsToSend)
        {
            card.GetComponent<CardFromHandAnimation>().SendCardToConveyorAnimation();
            column.Remove(card);

            yield return new WaitForSeconds(_delayBetweenCards);
        }

        // ���������� ���������� ����� �����
        RearrangeCardsInColumn(column, columnStartPoint);
    }

    // ����� ��� ������������ ���� � �������
    private void RearrangeCardsInColumn(List<Card> column, Transform startPoint)
    {
        for (int i = 0; i < column.Count; i++)
        {
            Card card = column[i];
            Vector3 targetPosition = startPoint.position + i * _cardSpacing;

            // ���������� �������� ��� �������� �����������
            StartCoroutine(MoveCardToPosition(card.transform, targetPosition, 0.3f));
        }
    }

    // �������� ��� �������� ����������� �����
    private IEnumerator MoveCardToPosition(Transform cardTransform, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = cardTransform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cardTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cardTransform.position = targetPosition;
    }    

}