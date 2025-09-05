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
    [SerializeField] private List<Card> _thirdColumn;
    [SerializeField] private List<Card> _fourthColumn; 
    [SerializeField] private List<Card> _fifthColumn;
    [SerializeField] private List<Card> _sixthColumn;
    private float _delayBetweenCards = 0.2f;
    [SerializeField] private Vector3 _cardSpacing = new Vector3(0, 0.2f, 0.1f); 
    [SerializeField] private Transform _firstColumnStartPoint; 
    [SerializeField] private Transform _secondColumnStartPoint;
    [SerializeField] private Transform _thirdColumnStartPoint;
    [SerializeField] private Transform _fourthColumnStartPoint;
    [SerializeField] private Transform _fifthColumnStartPoint;
    [SerializeField] private Transform _sixthColumnStartPoint;



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
        if(_firstColumn.Count > 0)
            RearrangeCardsInColumn(_firstColumn,_firstColumnStartPoint);

        if (_secondColumn.Count > 0)
            RearrangeCardsInColumn(_secondColumn, _secondColumnStartPoint);

        if (_thirdColumn.Count > 0)
            RearrangeCardsInColumn(_thirdColumn, _thirdColumnStartPoint);

        if (_fourthColumn.Count > 0)
            RearrangeCardsInColumn(_fourthColumn, _fourthColumnStartPoint);
        
        if (_fifthColumn.Count > 0)
            RearrangeCardsInColumn(_fifthColumn, _fifthColumnStartPoint);
       
        if(_sixthColumn.Count > 0)
            RearrangeCardsInColumn(_sixthColumn, _sixthColumnStartPoint);
    }

    public List<Card> GetFirstColumn() => _firstColumn;
    public List<Card> GetSecondColumn() => _secondColumn;
    public List<Card> GetThirdColumn() => _thirdColumn;
    public List<Card> GetFourthColumn() => _fourthColumn;

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
        else if (_thirdColumn.Contains(clickedCard))
        {
            StartCoroutine(SendMatchingCardsWithDelay(_thirdColumn, targetCardName, _thirdColumnStartPoint));
           
        }
        else if (_fourthColumn.Contains(clickedCard))
        {
            StartCoroutine(SendMatchingCardsWithDelay(_fourthColumn, targetCardName, _fourthColumnStartPoint));
        }
        else if (_fifthColumn.Contains(clickedCard))
        {
            StartCoroutine(SendMatchingCardsWithDelay(_fifthColumn, targetCardName, _fifthColumnStartPoint));
        }
        else if (_sixthColumn.Contains(clickedCard))
        {
            StartCoroutine(SendMatchingCardsWithDelay(_sixthColumn, targetCardName, _sixthColumnStartPoint));
        }
        else
        { 
            Debug.LogWarning("Clicked card not found in any column");
        }
    }

    private IEnumerator SendMatchingCardsWithDelay(List<Card> column, string targetName, Transform columnStartPoint)
    {
        List<Card> cardsToSend = new List<Card>();

        // Собираем карты для отправки
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

        // Отправляем карты на конвейер
        foreach (var card in cardsToSend)
        {
            AudioManager.Instance.Play("Sent");

            card.GetComponent<CardFromHandAnimation>().SendCardToConveyorAnimation();
            column.Remove(card);

            yield return new WaitForSeconds(_delayBetweenCards);
        }

        // Перемещаем оставшиеся карты вверх
        RearrangeCardsInColumn(column, columnStartPoint);
    }

    // Метод для перестановки карт в колонке
    private void RearrangeCardsInColumn(List<Card> column, Transform startPoint)
    {
        for (int i = 0; i < column.Count; i++)
        {
            Card card = column[i];
            Vector3 targetPosition = startPoint.position + i * _cardSpacing;

            // Используем анимацию для плавного перемещения
            StartCoroutine(MoveCardToPosition(card.transform, targetPosition, 0.3f));
        }
    }

    // Корутина для плавного перемещения карты
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