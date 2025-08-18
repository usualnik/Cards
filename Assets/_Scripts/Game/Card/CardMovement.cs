using UnityEngine;
using UnityEngine.Splines;

public class CardMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.2f;    

    private float progress = 0f;

    private SplineContainer _splineContainer;

    private Card _card;
    private bool _canMove = true;


    private void Awake()
    {
        _card = GetComponent<Card>();
    }
    private void Start()
    {
        //Refs
        _splineContainer = GameObject.FindAnyObjectByType<SplineContainer>();

        //Events
        _card.OnCardStateChanged += Card_OnCardStateChanged;
              
    }
    private void OnDestroy()
    {
        _card.OnCardStateChanged -= Card_OnCardStateChanged;
    }

    private void Card_OnCardStateChanged(Card.CardState cardState)
    {
        _canMove = cardState == Card.CardState.OnConveyor ? true : false;
    }

    private void Update()
    {
        if (_canMove) 
        {
            Move();
        }        
    }

    private void Move()
    {
        progress = Mathf.Repeat(progress + speed * Time.deltaTime, 1f);

        Vector3 position = _splineContainer.EvaluatePosition(progress);
        Vector3 tangent = _splineContainer.EvaluateTangent(progress);

        transform.position = position;
        transform.right = tangent;
    }
   
}
