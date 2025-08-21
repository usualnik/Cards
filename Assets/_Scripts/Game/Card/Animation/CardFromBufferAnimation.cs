using UnityEngine;

public class CardFromBufferAnimation : MonoBehaviour
{
    private Transform _recieverTransform;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Vector3 _initialScale;
    private Transform _initialParent;

    [Header("Animation Settings")]
    public float flightDuration = 1.5f;
    public float horizontalOffset = 2f; // Смещение по горизонтали вместо высоты
    public AnimationCurve flightCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 1, 1, 0.8f);
    public AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 180f);

    private bool _isAnimating = false;
    private float _animationProgress = 0f;

    private void Start()
    {
        _recieverTransform = FindAnyObjectByType<Receiver>().transform;
    }

    public void SendCardToConveyorAnimation()
    {
        if (_isAnimating || _recieverTransform == null) return;

        // Сохраняем начальные параметры
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialScale = transform.localScale;
        _initialParent = transform.parent;

        // Отключаем физику если есть
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Отключаем коллайдеры на время анимации
        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        var collider2D = GetComponent<Collider2D>();
        if (collider2D != null)
        {
            collider2D.enabled = false;
        }

        _isAnimating = true;
        _animationProgress = 0f;
    }

    private void Update()
    {
        if (!_isAnimating) return;

        _animationProgress += Time.deltaTime / flightDuration;

        if (_animationProgress >= 1f)
        {
            CompleteAnimation();
            return;
        }

        AnimateCard();
    }

    private void AnimateCard()
    {
        float curveValue = flightCurve.Evaluate(_animationProgress);
        float scaleValue = scaleCurve.Evaluate(_animationProgress);
        float rotationValue = rotationCurve.Evaluate(_animationProgress);

        // Движение по синусоидальной траектории в 2D
        Vector3 currentPosition = Calculate2DPosition(_animationProgress);

        // Плавное вращение
        Quaternion targetRotation = _initialRotation * Quaternion.Euler(0, 0, rotationValue);

        transform.position = currentPosition;
        transform.rotation = Quaternion.Slerp(_initialRotation, targetRotation, curveValue);
        transform.localScale = _initialScale * scaleValue;
    }

    private Vector3 Calculate2DPosition(float progress)
    {
        Vector3 startPos = _initialPosition;
        Vector3 endPos = _recieverTransform.position;

        // Синусоидальная траектория в 2D (вправо/влево)
        Vector3 direction = (endPos - startPos).normalized;
        Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0); // Перпендикулярный вектор

        float horizontalMovement = Mathf.Sin(progress * Mathf.PI) * -horizontalOffset;

        return Vector3.Lerp(startPos, endPos, progress) + perpendicular * horizontalMovement;
    }

    private void CompleteAnimation()
    {
        _isAnimating = false;

        // Фиксируем конечную позицию
        transform.position = _recieverTransform.position;
        transform.localScale = _initialScale * scaleCurve.Evaluate(1f);

        // Включаем коллайдеры обратно
        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        var collider2D = GetComponent<Collider2D>();
        if (collider2D != null)
        {
            collider2D.enabled = true;
        }

        


        transform.Rotate(0, 90, 0);
        gameObject.GetComponent<Card>().ChangeCardState(Card.CardState.OnConveyor);
        gameObject.GetComponent<CardMovement>().enabled = true;
    }

}